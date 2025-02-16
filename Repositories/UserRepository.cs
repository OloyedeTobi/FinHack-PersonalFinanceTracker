using Dapper;
using FinanceTracker.Models;
using System.Data;
using System.Threading.Tasks;

namespace FinanceTracker.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<UserComplete> GetUserById(int userid)
        {
            var sql = "SELECT * FROM TutorialAPISchema.Users WHERE UserId = @UserId";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserComplete>(sql, new { UserId = userid });
        }

        public async Task UpdateUser(UserComplete User)
        {
            var sql = "UPDATE TutorialAPISchema.Users SET FirstName = @LastName, Email = @Email, Gender = @Gender, JobTitle = @JobTitle, Active = @Active WHERE UserId = @UserId";
            await _dbConnection.ExecuteAsync(sql, User);
        }

        public async Task DeleteUser(int userid)
        {
            var sql = "DELETE FROM TutorialAPISchema.Users WHERE UserId = @UserId";
            await _dbConnection.ExecuteAsync(sql, new { UserId = userid });
        }
    }
}
