
using System.Data;
using Dapper;
using FinanceTracker.DTOs;
using FinanceTracker.Models;

namespace FinanceTracker.Repositories
{
    public class AuthRepository{

        private readonly IDbConnection _dbConnection;

        public AuthRepository(IDbConnection dbConnection){
            _dbConnection = dbConnection;
        }

        public async Task<UserRegistrationDTO?> CheckUserExists(string Email){
            var sql = "SELECT Email FROM TutorialAPISchema.Auth WHERE Email = @Email";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserRegistrationDTO>(sql, new { Email });
        }

        public async Task<UserLoginConfirmationDTO?> GetUserCredentials(string Email){
            var sql = @"EXEC TutorialAPISchema.spLoginConfirmation_Get @Email = @Email";

            return await _dbConnection.QuerySingleOrDefaultAsync<UserLoginConfirmationDTO>(sql, new { Email });
        }

        public async Task<int> GetUserId(string Email){
            var sql =  @"SELECT UserId FROM TutorialAPISchema.Users WHERE Email = @Email";
            
            return await _dbConnection.QuerySingleOrDefaultAsync<int>(sql, new { Email });

        }

    }
}