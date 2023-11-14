namespace HospitalRegistrar.Features.PatientFeatures;

public record UpdatePatientDto
{
    public int Id { get; set; }
    
    public int Age { get; set; }

    public string Name { get; set; } = default!;
}