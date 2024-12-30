using System.ComponentModel.DataAnnotations;

namespace Shary.API.Dtos;

public class RegisterDto
{
    [Required]
    public string DisplayName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*[a-z]{1,})(?=.*[A-Z]{1,})(?=.*\d{1,})(?=.*[!@#$%^&*]).{6,}$", ErrorMessage = "Password is invalid.")]
    public string Password { get; set; }
}
