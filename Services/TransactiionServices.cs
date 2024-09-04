using FinanceTracker.Models;
using FinanceTracker.Repositories;
using System.Threading.Tasks;

namespace FinanceTracker.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionService(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _transactionRepository.GetTransactionById(id);
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            await _transactionRepository.CreateTransaction(transaction);
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            await _transactionRepository.UpdateTransaction(transaction);
        }

        public async Task DeleteTransaction(int id)
        {
            await _transactionRepository.DeleteTransaction(id);
        }
    }
}
