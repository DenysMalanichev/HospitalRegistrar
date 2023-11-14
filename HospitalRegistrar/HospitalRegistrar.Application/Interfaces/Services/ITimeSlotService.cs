using HospitalRegistrar.Features.TimeSlotFeatures;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface ITimeSlotService
{
    Task<IEnumerable<GetTimeSlotDto>> GetAllVacantTimeSlotsForSpecialityAsync(string speciality);
    
    Task<IEnumerable<GetTimeSlotDto>> GetAllVacantTimeSlotsForDoctorAsync(int doctorId);

    Task<IEnumerable<GetTimeSlotDto>> GetAllTimeSlotsAsync();
    
    Task<GetTimeSlotDto> GetTimeSlotByIdAsync(int id);
    
    Task<GetTimeSlotDto> AddNewTimeSlotAsync(CreateTimeSlotDto createTimeSlotDto);
    
    Task<GetTimeSlotDto> UpdateTimeSlotAsync(UpdateTimeSlotDto updateTimeSlotDto);

    Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id);
}