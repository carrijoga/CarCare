using CarCare.Domain.DTO;
using CarCare.Domain.Entities.Persons;
using Microsoft.AspNetCore.Identity;

namespace CarCare.Domain.Entities.Users;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
        IsActive = true;
        CreatedAt = DateTime.Now;
    }

    #region Proprieties

    public Guid Id { get; set; }
    public int UserId { get; set; }
    public int PersonId { get; set; }
    
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; } // This will be an encrypted password
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Person? Person { get; set; }
    
    #endregion

    #region Methods

    public User CreateNewUser(UserRegisterDto userRegisterInfo) => 
        new()
        {
            Email = userRegisterInfo.Email,
            Password = new PasswordHasher<UserRegisterDto>()
                .HashPassword(userRegisterInfo, userRegisterInfo.Password),
            Username = userRegisterInfo.Username,
            Person = new Person().CreateNewPerson(userRegisterInfo),
        };
    
    #endregion
}
