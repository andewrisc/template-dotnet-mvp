using System;
using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    Task<User?> GetCurrentUserId(int Id);

    Task<User?> GetByEmailAndPasswordAsync(string Email, string Password);
}
