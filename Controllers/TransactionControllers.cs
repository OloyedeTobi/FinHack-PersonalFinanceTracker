using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Models;
using FinanceTracker.Services;
using System.Threading.Tasks;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetTransactionById/{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction transaction)
        {
            await _transactionService.CreateTransaction(transaction);
            return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
        }

        [HttpPut("UpdateTransaction/{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, Transaction transaction)
        {
            if (id != transaction.Id)
                return BadRequest();

            await _transactionService.UpdateTransaction(transaction);
            return NoContent();
        }

        [HttpDelete("DeleteTransaction/{id}")]
            public async Task<IActionResult> DeleteTransaction(int id)
        {
            await _transactionService.DeleteTransaction(id);
            return NoContent();
        }
    }
}
