using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarCare.Domain.Entities.Users;
using CarCare.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CarCare.Application.Authentication;

public class TokenAuthApplication
{
    private readonly IConfiguration _configuration;
    private readonly Context _context;

    public TokenAuthApplication(Context context, IConfiguration configuration)
    {
        _configuration = configuration;
        _context = context;
    }
    
    public string GenerateToken(User user, DateTime expires)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(user),
            Expires = expires,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtSecurityKey"]!)), 
                SecurityAlgorithms.HmacSha256Signature),
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private ClaimsIdentity GenerateClaims(User user)
    {
        return new ClaimsIdentity(
        [
            new Claim(nameof(User.Username), user.Username),
            new Claim(ClaimTypes.Name, user.Person.GetFullName()),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, _configuration["Jwt:JwtIssuer"]!),
            new Claim(JwtRegisteredClaimNames.Aud, _configuration["Jwt:JwtAudience"]!)
        ]);
    }
}