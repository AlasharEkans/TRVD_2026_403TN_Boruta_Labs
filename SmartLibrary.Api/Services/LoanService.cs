using AutoMapper;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Entities;
using SmartLibrary.Api.Repositories;

namespace SmartLibrary.Api.Services;

public class LoanService(ILoanRepository loanRepository, IMapper mapper) : ILoanService
{
    public async Task<IEnumerable<LoanDto>> GetActiveLoansAsync()
    {
        var loans = await loanRepository.GetAllActiveLoansAsync();
        return mapper.Map<IEnumerable<LoanDto>>(loans);
    }

    public async Task<LoanDto> IssueBookAsync(CreateLoanDto dto)
    {
        // Бізнес-логіка: Не можна видати книгу менш ніж на 1 день або більш ніж на 30
        if (dto.DaysToBorrow < 1 || dto.DaysToBorrow > 30)
            throw new ArgumentException("Термін оренди має бути від 1 до 30 днів.");

        var loan = new Loan
        {
            UserId = dto.UserId,
            BookId = dto.BookId,
            LoanDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(dto.DaysToBorrow)
        };

        await loanRepository.AddAsync(loan);
        return mapper.Map<LoanDto>(loan);
    }

    public async Task ReturnBookAsync(int loanId)
    {
        var loan = await loanRepository.GetByIdAsync(loanId);
        if (loan == null) throw new KeyNotFoundException("Запис про оренду не знайдено.");
        if (loan.ReturnDate != null) throw new InvalidOperationException("Книга вже повернута.");

        loan.ReturnDate = DateTime.UtcNow;
        await loanRepository.UpdateAsync(loan);
    }
}