using Microsoft.AspNetCore.Mvc;
using FinanceTracker.Models;
using FinanceTracker.Services;
using System.Threading.Tasks;

namespace FinanceTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly BudgetService _budgetService;

        public BudgetController(BudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet("GetBudgetById/{id}")]
        public async Task<IActionResult> GetBudget(int id)
        {
            var budget = await _budgetService.GetBudgetById(id);
            if (budget == null)
                return NotFound();

            return Ok(budget);
        }

        [HttpPost("CreateBudget")]
        public async Task<IActionResult> CreateBudget(Budget budget)
        {
            await _budgetService.CreateBudget(budget);
            return CreatedAtAction(nameof(GetBudget), new { id = budget.Id }, budget);
        }

        [HttpPut("UpdateBudget/{id}")]
        public async Task<IActionResult> UpdateBudget(int id, Budget budget)
        {
            if (id != budget.Id)
                return BadRequest();

            await _budgetService.UpdateBudget(budget);
            return NoContent();
        }

        [HttpDelete("DeleteBudget/{id}")]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            await _budgetService.DeleteBudget(id);
            return NoContent();
        }
    }
}
