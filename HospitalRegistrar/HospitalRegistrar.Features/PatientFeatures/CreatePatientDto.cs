namespace HospitalRegistrar.Features.PatientFeatures;

public record CreatePatientDto
{
    public int Age { get; set; }

    public string Name { get; set; } = default!;
}