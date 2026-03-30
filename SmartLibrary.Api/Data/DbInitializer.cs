using SmartLibrary.Api.Entities;
using BC = BCrypt.Net.BCrypt;

namespace SmartLibrary.Api.Data;

public static class DbInitializer
{
    public static async Task SeedAdminUser(LibraryDbContext context)
    {
        
        if (context.Users.Any()) return;

        
        var admin = new User
        {
            Email = "admin@library.com",
            PasswordHash = BC.HashPassword("Admin123!"), 
            Role = "Admin"
        };

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}