﻿namespace CarCare.Shared.Account;

public class UserRegister
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; } 
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public bool AcceptTerms { get; set; }
}