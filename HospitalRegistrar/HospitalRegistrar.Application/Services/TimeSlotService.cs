using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.TimeSlotFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;
using LinqKit;

namespace HospitalRegistrar.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly IPatientService _patientService;
    private readonly ITimeSlotsRepository _timeSlotsRepository;
    private readonly IDoctorsService _doctorsService;
    private readonly IMapper _mapper;

    public TimeSlotService(
        IPatientService patientService,
        ITimeSlotsRepository timeSlotsRepository,
        IDoctorsService doctorsService,
        IMapper mapper
        )
    {
        _patientService = patientService;
        _timeSlotsRepository = timeSlotsRepository;
        _doctorsService = doctorsService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetTimeSlotDto>> GetTimeSlotsBySpecificationsAsync(AvailableTimeSlotsByCriteriaRequestDto criteriaRequestDto)
    {
        DoctorSpecialtySpecification specialtySpec = null!;
        DoctorNameSpecification nameSpec = null!;
        TimeSlotDateSpecification dateSpec = null!;
        
        if (!string.IsNullOrEmpty(criteriaRequestDto.Specialty))
        {
            specialtySpec = new DoctorSpecialtySpecification(criteriaRequestDto.Specialty);
        }

        if (!string.IsNullOrEmpty(criteriaRequestDto.DoctorName))
        {
            nameSpec = new DoctorNameSpecification(criteriaRequestDto.DoctorName);
        }

        if (criteriaRequestDto.Date.HasValue)
        {
            dateSpec = new TimeSlotDateSpecification(criteriaRequestDto.Date.Value);
        }

        var predicate = PredicateBuilder.New<TimeSlot>(false);
        
        if (specialtySpec != null)
            predicate = predicate.And(specialtySpec.Criteria);

        if (nameSpec != null)
            predicate = predicate.And(nameSpec.Criteria);

        if (dateSpec != null)
            predicate = predicate.And(dateSpec.Criteria);

        var timeSlots = await _timeSlotsRepository.GetAvailableTimeSlotsByPredicateAsync(predicate);

        return _mapper.Map<IEnumerable<GetTimeSlotDto>>(timeSlots);
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