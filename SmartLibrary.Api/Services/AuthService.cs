using Microsoft.IdentityModel.Tokens;
using SmartLibrary.Api.Data;
using SmartLibrary.Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace SmartLibrary.Api.Services;


public class AuthService(IConfiguration config, LibraryDbContext context) : IAuthService
{
    public async Task<string> RegisterAsync(string email, string password, string role)
    {
        var user = new User
        {
            Email = email,
            PasswordHash = BC.HashPassword(password),
            Role = string.IsNullOrWhiteSpace(role) ? "Reader" : role // Якщо роль порожня — буде Reader
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return $"User with role '{user.Role}' registered successfully";
    }

    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

        // Перевірка хешу пароля
        if (user == null || !BC.Verify(password, user.PasswordHash))
            return null;

        return GenerateJwtToken(user);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.Role, user.Role), // Роль додається в токен
            new("userId", user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1), // Термін дії
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}