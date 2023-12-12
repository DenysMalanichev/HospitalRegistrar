using HospitalRegistrar.Features.Common;
using HospitalRegistrar.Features.TimeSlotFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface ITimeSlotService
{
    Task<GenericPagingDto<GetTimeSlotDto>> GetTimeSlotsBySpecificationsAsync(AvailablePagedTimeSlotsByCriteriaRequestDto criteriaRequestDto);

    Task<IEnumerable<GetTimeSlotDto>> GetAllTimeSlotsAsync();
    
    Task<GetTimeSlotDto> GetTimeSlotByIdAsync(int id);
    
    Task<GetTimeSlotDto> AddNewTimeSlotAsync(CreateTimeSlotDto createTimeSlotDto);
    
    Task<GetTimeSlotDto> UpdateTimeSlotAsync(UpdateTimeSlotDto updateTimeSlotDto);

    Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id);
}