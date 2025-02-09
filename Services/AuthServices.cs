using FinanceTracker.DTOs;
using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services{
    public class AuthService{
        private readonly AuthRepository _authRepository;

        public  AuthService(AuthRepository authRepository){
            _authRepository = authRepository;
        }


        public async Task<UserRegistrationDTO> CheckUserExists(string Email){
            return await _authRepository.CheckUserExists(Email);
        }

        public async Task<UserLoginConfirmationDTO> GetuserCredentials(string Email){
            return await _authRepository.GetUserCredentials(Email);
        }

         public async Task<int> GetUserId(string Email){
            return await _authRepository.GetUserId(Email);
        }
    }
}