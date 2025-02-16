using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker.Services{
    public class UserService{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository){
        _userRepository= userRepository;

    }

    public async Task<UserComplete> GetUserId(int userid){
            return await _userRepository.GetUserById(userid);
    }

    public async Task  UpdateUser(UserComplete user){
        await _userRepository.UpdateUser(user);
    }

    public async Task DeleteUser(int userid){
        await _userRepository.DeleteUser(userid);
    }
}

}