using System.ComponentModel.DataAnnotations;

namespace KarnelTravelGuide.Api.Contracts;

public class LoginRequest
{
    [Required(ErrorMessage = "Please enter your username.")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your password.")]
    public string Password { get; set; } = string.Empty;
}
