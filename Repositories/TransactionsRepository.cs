using Dapper;
using FinanceTracker.Models;
using System.Data;
using System.Threading.Tasks;

namespace FinanceTracker.Repositories
{
    public class TransactionRepository
    {
        private readonly IDbConnection _dbConnection;

        public TransactionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            var sql = "SELECT * FROM TutorialAPISchema.Transactions WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Transaction>(sql, new { Id = id });
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            var sql = "INSERT INTO TutorialAPISchema.Transactions (AccountId, UserId, Amount, Description, Date, Category, IsRecurring, Frequency) " +
                      "VALUES (@AccountId, @UserId, @Amount, @Description, @Date, @Category, @IsRecurring, @Frequency)";
            await _dbConnection.ExecuteAsync(sql, transaction);
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            var sql = "UPDATE TutorialAPISchema.Transactions SET AccountId = @AccountId, UserId = @UserId, Amount = @Amount, " +
                      "Description = @Description, Date = @Date, Category = @Category, IsRecurring = @IsRecurring, Frequency = @Frequency " +
                      "WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, transaction);
        }

        public async Task DeleteTransaction(int id)
        {
            var sql = "DELETE FROM TutorialAPISchema.Transactions WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
