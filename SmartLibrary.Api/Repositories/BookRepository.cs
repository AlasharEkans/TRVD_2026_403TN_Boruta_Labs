using Microsoft.EntityFrameworkCore;
using SmartLibrary.Api.Data;
using SmartLibrary.Api.Entities;

namespace SmartLibrary.Api.Repositories;

public class BookRepository(LibraryDbContext context) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync()
        => await context.Books.ToListAsync();

    public async Task<Book?> GetByIdAsync(int id)
        => await context.Books.FindAsync(id);

    public async Task AddAsync(Book book)
    {
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        context.Books.Update(book);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await GetByIdAsync(id);
        if (book != null)
        {
            context.Books.Remove(book);
            await context.SaveChangesAsync();
        }
    }
}