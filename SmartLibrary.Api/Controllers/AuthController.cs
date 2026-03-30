using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Api.DTOs;
using SmartLibrary.Api.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        return Ok(await authService.RegisterAsync(dto.Email, dto.Password, dto.Role));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await authService.LoginAsync(dto.Email, dto.Password);
        return token == null ? Unauthorized("Невірний email або пароль") : Ok(new { Token = token });
    }
}