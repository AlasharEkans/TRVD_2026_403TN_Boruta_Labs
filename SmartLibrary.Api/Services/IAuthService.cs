using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SmartLibrary.Api.Entities;
using BC = BCrypt.Net.BCrypt;
using Microsoft.EntityFrameworkCore;

namespace SmartLibrary.Api.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(string email, string password, string role);
    Task<string?> LoginAsync(string email, string password);
}