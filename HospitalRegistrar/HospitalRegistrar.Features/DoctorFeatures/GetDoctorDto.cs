namespace HospitalRegistrar.Features.DoctorFeatures;

public class GetDoctorDto
{
    public int Id { get; set; }
    
    public string Position { get; set; } = default!;

    public string Name { get; set; } = default!;
}