using System;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {

        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == password);
    }

    public async Task<User?> GetCurrentUserId(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
