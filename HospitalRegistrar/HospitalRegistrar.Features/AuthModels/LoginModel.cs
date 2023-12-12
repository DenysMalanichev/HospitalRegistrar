using System.ComponentModel.DataAnnotations;

namespace HospitalRegistrar.Features.AuthModels;

public class LoginModel
{
    [EmailAddress]
    [Required(ErrorMessage = "User Email is required")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = default!;
}