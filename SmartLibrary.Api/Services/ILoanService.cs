using SmartLibrary.Api.DTOs;

namespace SmartLibrary.Api.Services;

public interface ILoanService
{
    Task<IEnumerable<LoanDto>> GetActiveLoansAsync();
    Task<LoanDto> IssueBookAsync(CreateLoanDto dto);
    Task ReturnBookAsync(int loanId);
}