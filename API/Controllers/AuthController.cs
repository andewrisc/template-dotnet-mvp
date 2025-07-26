using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using API.Models.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers;

public class AuthController : BaseController
{
    private readonly IUserRepository _repo;
    private readonly IUserService _userService;
    private readonly IConfiguration _config;

    public AuthController(IUserRepository repo, IUserService userService, IConfiguration config)
    {
        _repo = repo;
        _userService = userService;
        _config = config;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _repo.GetByEmailAndPasswordAsync(login.Email, login.Password);
        if (user == null) return Unauthorized();

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null) return Unauthorized();

        return Ok(new { user.Id, user.Email, user.Username });
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var existingUser = await _repo.GetByEmailAsync(dto.Email);
        if (existingUser != null)
            return BadRequest("Email sudah digunakan.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var newUser = new User
        {
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Username = dto.UserName
        };

        await _repo.AddAsync(newUser);
        await _repo.SaveChangesAsync();
        return Ok("Akun berhasil dibuat");
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
