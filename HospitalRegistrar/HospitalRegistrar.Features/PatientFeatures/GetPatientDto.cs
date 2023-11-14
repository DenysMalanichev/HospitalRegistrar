namespace HospitalRegistrar.Features.PatientFeatures;

public class GetPatientDto
{
    public int Id { get; set; }
    
    public string Position { get; set; } = default!;

    public string Name { get; set; } = default!;
}