using System;
using API.Entities;

namespace API.Interfaces;

public interface IUserService
{
    Task<User?> GetCurrentUserAsync();
}
