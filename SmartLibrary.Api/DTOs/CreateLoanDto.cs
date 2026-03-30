namespace SmartLibrary.Api.DTOs;

public record CreateLoanDto(int UserId, int BookId, int DaysToBorrow);