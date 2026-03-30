namespace SmartLibrary.Api.DTOs;

public record RegisterDto(string Email, string Password, string Role);
public record LoginDto(string Email, string Password);
public record TokenDto(string Token, string Email, string Role);