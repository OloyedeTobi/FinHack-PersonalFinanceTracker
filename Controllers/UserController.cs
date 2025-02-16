using FinanceTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinanceTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly UserService _userService;

        public UserController (UserService userService)
        {
            _userService = userService;
        }

    [HttpGet("GetUserById/{userid}")]

    public  async Task<IActionResult> GetUser(int userid)
    {
        var user = await _userService.GetUserId(userid);
        if (user == null){
            return NotFound();
        }
        return Ok(user);

    }   


    [HttpPut("UpdateUser/{userid}")]
    public async Task<IActionResult> UpdateUser(int userid, UserComplete user)
    {
        if (userid != user.UserId){
            return BadRequest();

        }
        await _userService.UpdateUser(user);
        return Ok($"user {user.UserId} Updated");

    }

    [HttpDelete("DeleteUser/{userid}")]
    public async Task<IActionResult> DeleteUser(int userid){      

       await _userService.DeleteUser(userid);
       return Ok($"user {userid} Deleted");

    }
    }

   
}


