using FinanceTracker.Data;
using FinanceTracker.DTOs;
using FinanceTracker.Helpers;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace FinanceTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IConfiguration config) : ControllerBase
    {
        private readonly DataContext _dapper = new(config);
        private readonly AuthHelper _authHelper = new(config);
        private readonly SqlQueries _sqlqueries = new(config);
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegistrationDTO, UserComplete>();
            }));

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(UserRegistrationDTO registrationDto)
        {
            if (registrationDto.Password != registrationDto.PasswordConfirm)
            {
                return BadRequest("Passwords do not match!");
            }

            const string checkUserExistsSql = "SELECT Email FROM TutorialAPISchema.Auth WHERE Email = @Email";
            var existingUsers = _dapper.QueryData<string>(checkUserExistsSql, new { registrationDto.Email });

            if (existingUsers.Any())
            {
                return BadRequest("User with this email already exists!");
            }

            var loginDTO = new UserLoginDTO
            {
                Email = registrationDto.Email,
                Password = registrationDto.Password
            };

            if (!_authHelper.UpdateUserPassword(loginDTO))
            {
                return StatusCode(500, "Failed to register user.");
            }

            var user = _mapper.Map<UserComplete>(registrationDto);
            user.Active = true;
            
            if (!_sqlqueries.UpsertUser(user))
            {
                return StatusCode(500, "Failed to add user.");
            }

            return Ok();
        }

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(UserLoginDTO loginDTO)
        {
            if (!_authHelper.UpdateUserPassword(loginDTO))
            {
                return StatusCode(500, "Failed to update password!");
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserLoginDTO loginDTO)
        {
            const string getUserCredentialsSql = @"EXEC TutorialAPISchema.spLoginConfirmation_Get @Email = @Email";
            var parameters = new { loginDTO.Email };
            var userConfirmation = _dapper.QuerySingleOrDefault<UserLoginConfirmationDTO>(getUserCredentialsSql, parameters);
            if (userConfirmation == null)
            {
                return StatusCode(401, "Invalid email or password!");
            }


            var hashedPassword = _authHelper.ComputePasswordHash(loginDTO.Password, userConfirmation.PasswordSalt);

            if (!hashedPassword.SequenceEqual(userConfirmation.PasswordHash))
            {
                return Unauthorized("Incorrect password!");
            }

            const string getUserIdSql = @"SELECT UserId FROM TutorialAPISchema.Users WHERE Email = @Email";
            int userId = _dapper.QuerySingle<int>(getUserIdSql, new { loginDTO.Email });

            var token = _authHelper.GenerateToken(userId);
            return Ok(new { token });
        }

        [HttpGet("RefreshToken")]
        public IActionResult RefreshToken()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized();
            }

            var token = _authHelper.GenerateToken(userId);
            return Ok(new { token });
        }
    }
}
