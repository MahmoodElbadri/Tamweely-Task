using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;

namespace Tamweely.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService _authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return Ok(result);
    }
}
