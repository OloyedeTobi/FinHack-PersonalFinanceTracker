using Dapper;
using FinanceTracker.Models;
using System.Data;
using System.Threading.Tasks;

namespace FinanceTracker.Repositories
{
    public class BudgetRepository
    {
        private readonly IDbConnection _dbConnection;

        public BudgetRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Budget> GetBudgetById(int id)
        {
            var sql = "SELECT * FROM Budgets WHERE Id = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Budget>(sql, new { Id = id });
        }

        public async Task CreateBudget(Budget budget)
        {
            var sql = "INSERT INTO Budgets (UserId, Category, Amount, StartDate, EndDate) " +
                      "VALUES (@UserId, @Category, @Amount, @StartDate, @EndDate)";
            await _dbConnection.ExecuteAsync(sql, budget);
        }

        public async Task UpdateBudget(Budget budget)
        {
            var sql = "UPDATE Budgets SET UserId = @UserId, Category = @Category, Amount = @Amount, " +
                      "StartDate = @StartDate, EndDate = @EndDate WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, budget);
        }

        public async Task DeleteBudget(int id)
        {
            var sql = "DELETE FROM Budgets WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
