using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Entities;
using SmartLibrary.Api.Repositories;
using SmartLibrary.Api.Services;
using SmartLibrary.Api.Data;

public class BookService(IBookRepository repository, IMapper mapper, LibraryDbContext context) : IBookService
{
    private readonly LibraryDbContext _context = context;

  

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
    {
        var books = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<BookDto>>(books);
    }

    public async Task<BookDto?> GetBookByIdAsync(int id)
    {
        var book = await repository.GetByIdAsync(id);
        return mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> CreateBookAsync(CreateBookDto dto)
    {
        var book = mapper.Map<Book>(dto);
        await repository.AddAsync(book);
        return mapper.Map<BookDto>(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await repository.GetByIdAsync(id);
        if (book == null) return false;

        await repository.DeleteAsync(id);
        return true;
    }

    public async Task UpdateBookAsync(int id, Book book)
    {
        
        var existingBook = await _context.Books.AnyAsync(b => b.Id == id);
        if (!existingBook)
        {
            throw new KeyNotFoundException("Книгу не знайдено");
        }

        
        _context.Entry(book).State = EntityState.Modified;

        
        await _context.SaveChangesAsync();
    }
}