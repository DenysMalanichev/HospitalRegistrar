namespace HospitalRegistrar.Features.PatientFeatures;

public class GetPatientDto
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int Age { get; set; } = default!;
}