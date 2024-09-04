using Dapper;
using FinanceTracker.Models;
using System.Data;
using System.Threading.Tasks;

namespace PersonalFinanceTracker.Repositories
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
            var sql = "SELECT * FROM Accounts WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Account>(sql, new { Id = id });
        }

        public async Task CreateAccount(Account account)
        {
            var sql = "INSERT INTO Accounts (UserId, Name, Balance, AccountType, CreatedDate) " +
                      "VALUES (@UserId, @Name, @Balance, @AccountType, @CreatedDate)";
            await _dbConnection.ExecuteAsync(sql, account);
        }

        public async Task UpdateAccount(Account account)
        {
            var sql = "UPDATE Accounts SET UserId = @UserId, Name = @Name, Balance = @Balance, " +
                      "AccountType = @AccountType WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, account);
        }

        public async Task DeleteAccount(int id)
        {
            var sql = "DELETE FROM Accounts WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
