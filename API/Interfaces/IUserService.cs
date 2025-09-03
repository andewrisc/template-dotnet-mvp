using System;
using API.Entities;
using API.Models.DTOs.User;

namespace API.Interfaces;

public interface IUserService
{
    Task<User?> GetCurrentUserAsync();
    Task<User?> GetByEmailAndPasswordAsync(string Email, string Password);
    Task<User?> GetByEmailAsync(string email);
    Task<User> RegisterAccount(RegisterDto user);
}
