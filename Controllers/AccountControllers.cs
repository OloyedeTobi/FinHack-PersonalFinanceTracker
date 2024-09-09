using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Models;
using FinanceTracker.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FinanceTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAccountbyID/{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _accountService.GetAccountById(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            await _accountService.CreateAccount(account);
            return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
        }

        [HttpPut("UpdateAccount/{id}")]
        public async Task<IActionResult> UpdateAccount(int id, Account account)
        {
            if (id != account.Id)
                return BadRequest();

            await _accountService.UpdateAccount(account);
            return Ok();
        }

        [HttpDelete("DeleteAccount/{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccount(id);
            return Ok();
        }
    }
}
