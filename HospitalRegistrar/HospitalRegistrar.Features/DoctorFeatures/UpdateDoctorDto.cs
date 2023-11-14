namespace HospitalRegistrar.Features.DoctorFeatures;

public record UpdateDoctorDto
{
    public int Id { get; set; }
    
    public string Position { get; set; } = default!;

    public string Name { get; set; } = default!;
}