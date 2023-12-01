using HospitalRegistrar.Features.DoctorFeatures;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface IDoctorsService
{
    Task<IEnumerable<GetDoctorDto>> GetAllDoctorsAsync();
    
    Task<GetDoctorDto> GetDoctorByIdAsync(int id);
    
    Task<GetDoctorDto> AddNewDoctorAsync(CreateDoctorDto createDoctorDto);

    Task<GetDoctorDto> AssociateWithTimeSlot(int doctorId, int timeSlotId);
    
    Task<GetDoctorDto> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto);

    Task<GetDoctorDto> DeleteDoctorAsync(int id);
}