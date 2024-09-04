using FinanceTracker.Models;
using FinanceTracker.Repositories;
using System.Threading.Tasks;

namespace FinanceTracker.Services
{
    public class BudgetService
    {
        private readonly BudgetRepository _budgetRepository;

        public BudgetService(BudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<Budget> GetBudgetById(int id)
        {
            return await _budgetRepository.GetBudgetById(id);
        }

        public async Task CreateBudget(Budget budget)
        {
            await _budgetRepository.CreateBudget(budget);
        }

        public async Task UpdateBudget(Budget budget)
        {
            await _budgetRepository.UpdateBudget(budget);
        }

        public async Task DeleteBudget(int id)
        {
            await _budgetRepository.DeleteBudget(id);
        }
    }
}
