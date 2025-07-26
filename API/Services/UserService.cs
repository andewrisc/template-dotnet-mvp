using System;
using System.Security.Claims;
using API.Entities;
using API.Interfaces;

namespace API.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _http;
    private readonly IUserRepository _repo;
    public UserService(IHttpContextAccessor http, IUserRepository repo)
    {
        _http = http;
        _repo = repo;
    }

    public async Task<User?> GetCurrentUserAsync()
    {
        var userIdClaim = _http.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return null;

        if (!int.TryParse(userIdClaim, out int userId)) return null;

        return await _repo.GetCurrentUserId(userId);
    }
}
