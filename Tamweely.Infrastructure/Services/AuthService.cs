using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tamweely.Application.DTOs;
using Tamweely.Application.Interfaces;
using Tamweely.Domain.Entities;

namespace Tamweely.Infrastructure.Services;

public class AuthService(UserManager<AppUser> userManager, IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponseDto> LoginAsync(LoginDto model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);

        if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
            return new AuthResponseDto { Message = "Invalid Email or Password" };

        var token = await CreateJwtToken(user);

        return new AuthResponseDto
        {
            Message = null,
            Username = user.FirstName + " " + user.LastName,
            Email = user.Email!,
            Token = token,
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto model)
    {
        if (await userManager.FindByEmailAsync(model.Email) is not null)
            return new AuthResponseDto { Message = "Email is already registered!" };


        var user = new AppUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.FirstName + "-" + model.LastName,
            Email = model.Email,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = string.Empty;
            foreach (var error in result.Errors)
                errors += $"{error.Description}, ";

            return new AuthResponseDto { Message = errors };
        }

        // Generate Token immediately after register (Optional, or make them login)
        // For now, let's return success message
        return new AuthResponseDto
        {
            Message = null,
            Email = user.Email,
            Username = model.FirstName + " " + model.LastName,
            Token = await CreateJwtToken(user), // Auto-login after register
        };
    }

    private async Task<string> CreateJwtToken(AppUser user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id), // User ID
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("uid", user.Id),
            new Claim("username", user.UserName!)
        }
        .Union(userClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(double.Parse(configuration["Jwt:DurationInDays"]!)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}
