using AutoMapper;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Entities;
using SmartLibrary.Api.Repositories;

namespace SmartLibrary.Api.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> GetAllBooksAsync();
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDto> CreateBookAsync(CreateBookDto dto);

    Task<bool> DeleteBookAsync(int id);
    Task UpdateBookAsync(int id, Book book);
}

