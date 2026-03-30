using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Entities;
using SmartLibrary.Api.Services;

namespace SmartLibrary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{

    private readonly IBookService _bookService = bookService;



    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
    {
        return Ok(await bookService.GetAllBooksAsync());
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
    {
        // Валідація ID
        if (id != updatedBook.Id)
        {
            return BadRequest("ID книги в URL та в тілі запиту не збігаються");
        }

        try
        {
            // Викликаємо наш сервіс
            await _bookService.UpdateBookAsync(id, updatedBook);
            return NoContent(); // Успішно оновлено (204)
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Книгу не знайдено в базі даних");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Помилка сервера: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook(int id)
    {
        var book = await bookService.GetBookByIdAsync(id);
        return book == null ? NotFound() : Ok(book);
    }

    [Authorize(Roles = "Admin,Librarian")]
    [HttpPost]
    public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto dto)
    {
        var result = await bookService.CreateBookAsync(dto);
        return CreatedAtAction(nameof(GetBook), new { id = result.Id }, result);
    }

    [Authorize(Roles = "Admin")] // Тільки адміністратор може видаляти книги
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await bookService.DeleteBookAsync(id);

        if (!deleted)
        {
            return NotFound($"Книгу з ID {id} не знайдено.");
        }

        return NoContent(); // Повертає 204 No Content — стандарт для успішного видалення
    }
}