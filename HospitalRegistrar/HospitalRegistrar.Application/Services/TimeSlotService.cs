using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.Common;
using HospitalRegistrar.Features.TimeSlotFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;

namespace HospitalRegistrar.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly ITimeSlotsRepository _timeSlotsRepository;
    private readonly IDataQueryingService<TimeSlot, GetTimeSlotDto> _dataQueryingService;
    private readonly IMapper _mapper;

    public TimeSlotService(
        ITimeSlotsRepository timeSlotsRepository,
        IMapper mapper,
        IDataQueryingService<TimeSlot, GetTimeSlotDto> dataQueryingService)
    {
        _timeSlotsRepository = timeSlotsRepository;
        _mapper = mapper;
        _dataQueryingService = dataQueryingService;
    }

    public async Task<GenericPagingDto<GetTimeSlotDto>> GetTimeSlotsBySpecificationsAsync(AvailablePagedTimeSlotsByCriteriaRequestDto criteriaRequestDto)
    {
        var specifications = new List<ISpecification<TimeSlot>>();
        
        if (!string.IsNullOrEmpty(criteriaRequestDto.Specialty))
        {
            specifications.Add(new DoctorSpecialtySpecification(criteriaRequestDto.Specialty));
        }

        if (!string.IsNullOrEmpty(criteriaRequestDto.DoctorName))
        {
             specifications.Add(new DoctorNameSpecification(criteriaRequestDto.DoctorName));
        }

        if (criteriaRequestDto.Date.HasValue)
        {
            specifications.Add(new TimeSlotDateSpecification(criteriaRequestDto.Date.Value));
        }

        var pagingParams = new QueryPaginationCriteria<TimeSlot>
        {
            CurrentPage = criteriaRequestDto.CurrentPage ?? 1,
            IsDescending = criteriaRequestDto.IsDescending,
            ItemsOnPage = 10,
            SortBy = timeSlot => timeSlot.TimeBegin,
        };
        
        var timeSlots = await _dataQueryingService.GetPagedEntityByCriteriaAsync(pagingParams, specifications.ToArray());

        return timeSlots;
    }

    public async Task<IEnumerable<GetTimeSlotDto>> GetAllTimeSlotsAsync()
    {
        var timeSlots = await _timeSlotsRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<GetTimeSlotDto>>(timeSlots);
    }

    public async Task<GetTimeSlotDto> GetTimeSlotByIdAsync(int id)
    {
        var timeSlot = await _timeSlotsRepository.GetByIdAsync(id)!
            ?? throw new EntityNotFoundException($"No Time Slot with Id '{id}'");

        return _mapper.Map<GetTimeSlotDto>(timeSlot);
    }

    public async Task<GetTimeSlotDto> AddNewTimeSlotAsync(CreateTimeSlotDto createTimeSlotDto)
    {
        var record = _mapper.Map<TimeSlot>(createTimeSlotDto);

        var createdRecord = await _timeSlotsRepository.AddAsync(record);

        return _mapper.Map<GetTimeSlotDto>(createdRecord);
    }

    public async Task<GetTimeSlotDto> UpdateTimeSlotAsync(UpdateTimeSlotDto updateTimeSlotDto)
    {
        if (await _timeSlotsRepository.GetByIdAsync(updateTimeSlotDto.Id)! is null)
        {
            throw new EntityNotFoundException($"No Time Slot with Id '{updateTimeSlotDto.Id}'");
        }

        var recordToUpdate = _mapper.Map<TimeSlot>(updateTimeSlotDto);

        var updatedRecord = await _timeSlotsRepository.UpdateAsync(updateTimeSlotDto.Id, recordToUpdate);
        
        return _mapper.Map<GetTimeSlotDto>(updatedRecord);
    }

    public async Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id)
    {
        var deletedRecord = await _timeSlotsRepository.DeleteAsync(id)!
                            ?? throw new EntityNotFoundException($"No Record with Id '{id}'");

        return _mapper.Map<GetTimeSlotDto>(deletedRecord);
    }
}