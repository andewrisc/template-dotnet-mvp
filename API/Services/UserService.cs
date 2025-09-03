using System;
using System.Security.Claims;
using API.Entities;
using API.Interfaces;
using API.Models.DTOs.User;

namespace API.Services;

public class UserService(IHttpContextAccessor http, IUserRepository repo) : IUserService
{
    protected readonly IHttpContextAccessor _http = http;
    protected readonly IUserRepository _repo = repo;

    public async Task<User?> GetByEmailAndPasswordAsync(string Email, string Password)
    {
        var user = await _repo.GetByEmailAndPasswordAsync(Email, Password);
        if (user == null) return null;

        bool isMatch = BCrypt.Net.BCrypt.Verify(Password, user.PasswordHash);

        return isMatch ? user : null;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _repo.GetByEmailAsync(email);
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var userIdClaim = _http.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return null;

        if (!int.TryParse(userIdClaim, out int userId)) return null;

        return await _repo.GetCurrentUserId(userId);
    }

    public async Task<User> RegisterAccount(RegisterDto registerDto)
    {

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        var newUser = new User
        {
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            Username = registerDto.UserName
        };

        await _repo.AddAsync(newUser);
        await _repo.SaveChangesAsync();

        return newUser;
    }
}
