using FinanceTracker.Models;
using FinanceTracker.Repositories;

namespace FinanceTracker;
public class UserService{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository){
        _userRepository= userRepository;

    }

    public async Task<UserComplete> GetUserId(string Email){
            return await _userRepository.GetUserById(Email);
    }

    public async Task  UpdateUser(UserComplete user){
        await _userRepository.UpdateUser(user);
    }

    public async Task DeleteUser(int id){
        await _userRepository.DeleteUser(id);
    }
}