using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.TimeSlotFeatures;

namespace HospitalRegistrar.Application.Services;

public class TimeSlotService : ITimeSlotService
{
    private readonly IPatientService _patientService;
    private readonly ITimeSlotsRepository _timeSlotsRepository;
    private readonly IDoctorsService _doctorsService;

    public TimeSlotService(IPatientService patientService, ITimeSlotsRepository timeSlotsRepository, IDoctorsService doctorsService)
    {
        _patientService = patientService;
        _timeSlotsRepository = timeSlotsRepository;
        _doctorsService = doctorsService;
    }

    public Task<IEnumerable<GetTimeSlotDto>> GetAllVacantTimeSlotsForSpecialityAsync(string speciality)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetTimeSlotDto>> GetAllVacantTimeSlotsForDoctorAsync(int doctorId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetTimeSlotDto>> GetAllTimeSlotsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetTimeSlotDto> GetTimeSlotByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GetTimeSlotDto> AddNewTimeSlotAsync(CreateTimeSlotDto createTimeSlotDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetTimeSlotDto> UpdateTimeSlotAsync(UpdateTimeSlotDto updateTimeSlotDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetTimeSlotDto> DeleteTimeSlotAsync(int id)
    {
        throw new NotImplementedException();
    }
}