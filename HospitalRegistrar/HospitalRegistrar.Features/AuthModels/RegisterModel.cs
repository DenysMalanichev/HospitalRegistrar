using System.ComponentModel.DataAnnotations;

namespace HospitalRegistrar.Features.AuthModels;

public class RegisterModel
{
    [Required(ErrorMessage = "User Name is required")]
    public string Name { get; set; } = default!;

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = default!;

    [Required(ErrorMessage = "Role is required")]
    public string Role { get; set; } = default!;
}