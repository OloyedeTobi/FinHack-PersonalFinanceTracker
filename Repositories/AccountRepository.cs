using Dapper;
using FinanceTracker.Models;
using System.Data;
using System.Threading.Tasks;

namespace FinanceTracker.Repositories
{
    public class AccountRepository
    {
        private readonly IDbConnection _dbConnection;

        public AccountRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Account> GetAccountById(int id)
        {
            var sql = "SELECT * FROM TutorialAPISchema.Accounts WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Account>(sql, new { Id = id });
        }

        public async Task CreateAccount(Account account)
        {
            var sql = "INSERT INTO TutorialAPISchema.Accounts (UserId, Name, Balance, AccountType, CreatedDate) " +
                      "VALUES (@UserId, @Name, @Balance, @AccountType, @CreatedDate)";
            await _dbConnection.ExecuteAsync(sql, account);
        }

        public async Task UpdateAccount(Account account)
        {
            var sql = "UPDATE TutorialAPISchema.Accounts SET UserId = @UserId, Name = @Name, Balance = @Balance, " +
                      "AccountType = @AccountType WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, account);
        }

        public async Task DeleteAccount(int id)
        {
            var sql = "DELETE FROM TutorialAPISchema.Accounts WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
