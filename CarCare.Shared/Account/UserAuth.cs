namespace CarCare.Shared.Account;

public class UserAuth
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public required string Password { get; set; }
    public bool? RememberMe { get; set; }
}