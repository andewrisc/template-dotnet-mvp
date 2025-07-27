using System;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetCurrentUserId(int Id);
    Task<User?> GetByEmailAndPasswordAsync(string Email, string Password);
    Task<User?> GetByEmailAsync(string email);
}
