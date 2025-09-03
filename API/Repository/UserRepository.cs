using System;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetCurrentUserId(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
