namespace CarCare.Domain.DTO;

public class UserRegisterDto
{
    #region Properties

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; } 
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? BirthDay { get; set; }
    public string? Cpf { get; set; }
    public string? Cnpj { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public bool AcceptTerms { get; set; }

    #endregion

    #region Methods

    public void IsValid()
    {
        if (AcceptTerms)
            throw new InvalidOperationException("Accept terms is required");

        if (string.IsNullOrWhiteSpace(FirstName)
         || string.IsNullOrWhiteSpace(LastName))
            throw new InvalidOperationException("First name and Last name are required");

        if (string.IsNullOrWhiteSpace(Username))
            throw new InvalidOperationException("Username is required");
        
        if (string.IsNullOrWhiteSpace(Email))
            throw new InvalidOperationException("Email is required");
        
        if (string.IsNullOrWhiteSpace(Password))
            throw new InvalidOperationException("Password is required");
        
        if (Password != ConfirmPassword)
            throw new InvalidOperationException("Password and Confirm password are different");
    }

    #endregion
}