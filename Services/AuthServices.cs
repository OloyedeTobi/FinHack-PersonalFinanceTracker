using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services{
    public class AuthService{
        private readonly AuthRepository _authRepository;

        public  AuthService(AuthRepository authRepository){
            _authRepository = authRepository;
        }


        public async Task<UserComplete> CheckUserExists(string Email){
            return await _authRepository.CheckUserExists(Email);
        }

        public async Task<UserComplete> GetuserCredentials(string Email){
            return await _authRepository.GetUserCredentials(Email);
        }

         public async Task<UserComplete> GetuserId(string Email){
            return await _authRepository.GetUserId(Email);
        }
    }
}