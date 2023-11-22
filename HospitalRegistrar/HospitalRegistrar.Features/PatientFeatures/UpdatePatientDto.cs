using System.ComponentModel.DataAnnotations;

namespace HospitalRegistrar.Features.PatientFeatures;

public record UpdatePatientDto
{
    [Required]
    public int Id { get; set; }
   
    [Required]
    [Range(0, 150)]
    public int Age { get; set; }

    [Required]
    [MinLength(2)]
    public string Name { get; set; } = default!;
}