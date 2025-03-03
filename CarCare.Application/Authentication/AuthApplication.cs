using System.Security.Authentication;
using CarCare.Domain.DTO;
using CarCare.Domain.Entities.Users;
using CarCare.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarCare.Application.Authentication;

public class AuthApplication
{
    readonly Context _context;
    readonly IConfiguration _configuration;

    public AuthApplication(Context context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task<UserAuthDto> Login(string? email, string? username, string password)
    {
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(username))
            throw new AuthenticationException("Email or username is required");
        
        var user = await _context.Users
            .Include(x => x.Person)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == email || x.Username == username).ConfigureAwait(false);
        
        if (user is null)
            throw new AuthenticationException("User not found");

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, password) 
            == PasswordVerificationResult.Failed)
            throw new AuthenticationException("Invalid password");

        var expires = DateTime.UtcNow.AddDays(1).Date;
        
        return new UserAuthDto
        {
            Token = new TokenAuthApplication(_context, _configuration).GenerateToken(user, expires),
            RefreshToken = null, //Future implementation
            Expires = expires,
            Message = "Login successful",
        };
    }

    public async Task<bool> Register(UserRegisterDto userRegisterInfo)
    {
        userRegisterInfo.IsValid();
        
        if (await _context.Users.AnyAsync(x => x.Email == userRegisterInfo.Email 
                                                 || x.Username == userRegisterInfo.Username).ConfigureAwait(false))
            throw new AuthenticationException("User already exists");
        
        _context.Users.Add(new User().CreateNewUser(userRegisterInfo));
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}