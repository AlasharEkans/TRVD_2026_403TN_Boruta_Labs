using AutoMapper;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Entities;
using SmartLibrary.Api.Repositories;
using SmartLibrary.Api.Services;

public class BookService(IBookRepository repository, IMapper mapper) : IBookService
{
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
}