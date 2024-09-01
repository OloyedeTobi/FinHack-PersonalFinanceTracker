using System.Security.Cryptography;
using System.Text;
using Dapper;
using FinanceTracker.Data;
using FinanceTracker.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinanceTracker.Helpers
{
    public class AuthHelper(IConfiguration config)
    {
        private readonly IConfiguration _config = config;
        private readonly DataContext _dapper = new(config);

        public byte[] ComputePasswordHash(string password, byte[] salt)
        {
            string key = _config.GetSection("AppSettings:PasswordKey").Value;
            string saltedKey = key + Convert.ToBase64String(salt);

            return KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(saltedKey),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000000,
                numBytesRequested: 32
            );
        }

        public string GenerateToken(int userId)
        {
            var claims = new Claim[]
            {
                new("userId", userId.ToString())
            };

            string tokenKey = _config.GetSection("AppSettings:TokenKey").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddDays(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }

        public bool UpdateUserPassword(UserLoginDTO loginDto)
        {
            var salt = GenerateSalt();
            var hashedPassword = ComputePasswordHash(loginDto.Password, salt);

            const string sqlCommand = @"EXEC TutorialAPISchema.spRegistration_Upsert
                @Email = @UserEmail, 
                @PasswordHash = @HashedPassword, 
                @PasswordSalt = @Salt";

            var parameters = new
            {
                UserEmail = loginDto.Email,
                HashedPassword = hashedPassword,
                Salt = salt
            };

            return _dapper.ExecuteCommand(sqlCommand, parameters);
        }
        private static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
