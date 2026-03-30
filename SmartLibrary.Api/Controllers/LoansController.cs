using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Services;

namespace SmartLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController(ILoanService loanService) : ControllerBase
{
    [HttpGet("active")]
    public async Task<ActionResult<IEnumerable<LoanDto>>> GetActiveLoans()
    {
        return Ok(await loanService.GetActiveLoansAsync());
    }

    [HttpPost("issue")]
    public async Task<ActionResult<LoanDto>> IssueBook(CreateLoanDto dto)
    {
        try
        {
            var result = await loanService.IssueBookAsync(dto);
            return Ok(result);
        }
        catch (ArgumentException ex) // Обробка нашої бізнес-помилки
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnBook(int id)
    {
        try
        {
            await loanService.ReturnBookAsync(id);
            return NoContent(); // 204 статус — успіх, але без тіла відповіді
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}