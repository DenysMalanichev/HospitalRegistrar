namespace HospitalRegistrar.Features.DoctorFeatures;

public record CreateDoctorDto
{
    public string Position { get; set; } = default!;

    public string Name { get; set; } = default!;
}