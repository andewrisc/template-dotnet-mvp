using System;

namespace API.Models.DTOs.User;

public class RegisterDto : LoginDto
{
    public string UserName { get; set; } = null!;
}
