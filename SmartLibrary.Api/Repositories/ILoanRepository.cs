using SmartLibrary.Api.Entities;

namespace SmartLibrary.Api.Repositories;

public interface ILoanRepository
{
    Task<IEnumerable<Loan>> GetAllActiveLoansAsync();
    Task AddAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task<Loan?> GetByIdAsync(int id);
}