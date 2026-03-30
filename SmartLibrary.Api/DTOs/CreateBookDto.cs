namespace SmartLibrary.Api.DTOs;


public record CreateBookDto(
    string Title,
    string? ISBN,
    int PublicationYear,
    string? Description
);