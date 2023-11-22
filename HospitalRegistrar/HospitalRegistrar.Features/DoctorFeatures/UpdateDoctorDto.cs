using System.ComponentModel.DataAnnotations;

namespace HospitalRegistrar.Features.DoctorFeatures;

public record UpdateDoctorDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MinLength(2)]
    public string Position { get; set; } = default!;

    [Required]
    [MinLength(2)]
    public string Name { get; set; } = default!;
}