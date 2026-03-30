using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Services;

namespace SmartLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        return Ok(await bookService.GetAllBooksAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await bookService.GetBookByIdAsync(id);
        return book == null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto dto)
    {
        var result = await bookService.CreateBookAsync(dto);
        return CreatedAtAction(nameof(GetBook), new { id = result.Id }, result);
    }
}