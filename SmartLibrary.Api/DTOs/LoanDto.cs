namespace SmartLibrary.Api.DTOs;

public record LoanDto(int Id, int UserId, int BookId, DateTime LoanDate, DateTime DueDate, DateTime? ReturnDate);