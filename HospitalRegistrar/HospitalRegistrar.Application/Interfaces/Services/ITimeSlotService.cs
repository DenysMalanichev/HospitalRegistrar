using HospitalRegistrar.Features.TimeSlotFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface ITimeSlotService
{
    Task<IEnumerable<GetTimeSlotDto>> GetTimeSlotsBySpecificationsAsync(AvailableTimeSlotsByCriteriaRequestDto criteriaRequestDto);

    Task<IEnumerable<GetTimeSlotDto>> GetAllTimeSlotsAsync();
    
    Task<GetTimeSlotDto> GetTimeSlotByIdAsync(int id);
    
    Task<GetTimeSlotDto> AddNewTimeSlotAsync(CreateTimeSlotDto createTimeSlotDto);
    
    Task<GetTimeSlotDto> UpdateTimeSlotAsync(UpdateTimeSlotDto updateTimeSlotDto);

    Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id);
}