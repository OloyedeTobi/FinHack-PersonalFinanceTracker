
using System.Data;
using Dapper;
using FinanceTracker.Models;

namespace FinanceTracker.Repositories
{
    public class AuthRepository{

        private readonly IDbConnection _dbConnection;

        public AuthRepository(IDbConnection dbConnection){
            _dbConnection = dbConnection;
        }

        public async Task<UserComplete> CheckUserExists(string Email){
            var sql = "SELECT Email FROM TutorialAPISchema.Auth WHERE Email = @Email";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserComplete>(sql, new { Email = Email });
        }

    }
}