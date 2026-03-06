using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces.Services;
using Application.Security;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Infrastructure.Security;

public class JwtTokenService(IConfiguration configuration) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    
    public TokenResult Token(User user)
    {
        var claims = CreateClaims(user);

        var tokenExpiration = TokenExpiration();
        var refreshTokenExpiration = RefreshTokenExpiration();
        
        var token = CreateJwtToken(claims, tokenExpiration);
        var refreshToken = RefreshToken();
        
        return new TokenResult(token, refreshToken, tokenExpiration, refreshTokenExpiration);
    }
    
    private List<Claim> CreateClaims(User user)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email.Address),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti.ToString(), Guid.NewGuid().ToString()),
        };
    }
    
    private string CreateJwtToken(List<Claim> claims, DateTime tokenExpiration)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims: claims,
            expires: tokenExpiration,
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    
    public string RefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
    
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!))
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        return principal;
    }

    public DateTime TokenExpiration()
    {
        _ = int.TryParse(_configuration["Jwt:ExpirationInMinutes"], out var expirationInMinutes);
        return DateTime.UtcNow.AddDays(expirationInMinutes == 0 ? 1 : expirationInMinutes);
    }

    public DateTime RefreshTokenExpiration()
    {
        _ = int.TryParse(_configuration["Jwt:RefreshTokenExpirationInDays"], out var expirationInDays);
        return DateTime.UtcNow.AddDays(expirationInDays == 0 ? 7 : expirationInDays);
    }
}