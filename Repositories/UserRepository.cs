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

        public async Task<UserComplete> GetUserById(string Email)
        {
            var sql = "SELECT * FROM TutorialAPISchema.Users WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<UserComplete>(sql, new { Email });
        }

        public async Task UpdateUser(UserComplete User)
        {
            var sql = "UPDATE TutorialAPISchema.Users SET UserId = @UserId, FirstName = @LastName, Email = @Email, Gender = @Gender, JobTitle = @JobTitle, Active = @Active WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, User);
        }

        public async Task DeleteUser(int id)
        {
            var sql = "DELETE FROM TutorialAPISchema.Users WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
