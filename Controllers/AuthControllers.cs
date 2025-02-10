using FinanceTracker.Data;
using FinanceTracker.DTOs;
using FinanceTracker.Helpers;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FinanceTracker.Services;
using System.Threading.Tasks;


namespace FinanceTracker.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly AuthService _authService;
    private readonly DataContext _dapper;
    private readonly AuthHelper _authHelper;
    private readonly SqlQueries _sqlqueries;
    private readonly IMapper _mapper;

    public AuthController(IConfiguration config, AuthService authService)
    {
        _config = config;
        _authService = authService;
        _dapper = new(config);
        _authHelper = new(config);
        _sqlqueries = new(config);
        _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegistrationDTO, UserComplete>();
            }));
    }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegistrationDTO registrationDto)
        {
            if (registrationDto.Password != registrationDto.PasswordConfirm)
            {
                return BadRequest("Passwords do not match!");
            }

            var existingUsers = await _authService.CheckUserExists( registrationDto.Email );


            if (existingUsers != null)
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
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            
            var userConfirmation = await _authService.GetuserCredentials(loginDTO.Email);

            if (userConfirmation == null)
            {
                return StatusCode(401, "Invalid email or password!");
            }


            var hashedPassword = _authHelper.ComputePasswordHash(loginDTO.Password, userConfirmation.PasswordSalt);

            if (!hashedPassword.SequenceEqual(userConfirmation.PasswordHash))
            {
                return Unauthorized("Incorrect password!");
            }
 
            int userId = await _authService.GetUserId(loginDTO.Email);

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
