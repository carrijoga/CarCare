namespace CarCare.Domain.DTO;

public class UserAuthDto
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? Expires { get; set; }
    public string? Message { get; set; }
}