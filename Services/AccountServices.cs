using FinanceTracker.Models;
using PersonalFinanceTracker.Repositories;
using System.Threading.Tasks;

namespace FinanceTracker.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _accountRepository.GetAccountById(id);
        }

        public async Task CreateAccount(Account account)
        {
            await _accountRepository.CreateAccount(account);
        }

        public async Task UpdateAccount(Account account)
        {
            await _accountRepository.UpdateAccount(account);
        }

        public async Task DeleteAccount(int id)
        {
            await _accountRepository.DeleteAccount(id);
        }
    }
}
