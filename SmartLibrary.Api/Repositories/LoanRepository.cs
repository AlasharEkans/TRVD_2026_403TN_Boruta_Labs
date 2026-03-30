using Microsoft.EntityFrameworkCore;
using SmartLibrary.Api.Data;
using SmartLibrary.Api.Entities;

namespace SmartLibrary.Api.Repositories;

public class LoanRepository(LibraryDbContext context) : ILoanRepository
{
    public async Task<IEnumerable<Loan>> GetAllActiveLoansAsync()
    {
        
        return await context.Loans.Where(l => l.ReturnDate == null).ToListAsync();
    }

    public async Task<Loan?> GetByIdAsync(int id) => await context.Loans.FindAsync(id);

    public async Task AddAsync(Loan loan)
    {
        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Loan loan)
    {
        context.Loans.Update(loan);
        await context.SaveChangesAsync();
    }
}