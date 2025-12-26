using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;

namespace Tamweely.Infrastructure.Services;

public class AuthService : IAuthService
{
    public Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }
}
