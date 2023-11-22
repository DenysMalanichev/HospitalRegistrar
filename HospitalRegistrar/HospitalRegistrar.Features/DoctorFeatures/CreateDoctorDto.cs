using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HospitalRegistrar.Features.DoctorFeatures;

public record CreateDoctorDto
{
    [Required]
    [MinLength(2)]
    public string Position { get; set; } = default!;

    [Required]
    [MinLength(2)]
    public string Name { get; set; } = default!;
}