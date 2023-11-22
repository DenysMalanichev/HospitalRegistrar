using System.ComponentModel.DataAnnotations;

namespace HospitalRegistrar.Features.PatientFeatures;

public record CreatePatientDto
{
    [Required]
    [Range(0, 150)]
    public int Age { get; set; }

    [Required]
    [MinLength(2)]
    public string Name { get; set; } = default!;
}