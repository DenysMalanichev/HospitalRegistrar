using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.RecordFeatures;

namespace HospitalRegistrar.Application.Services;

public class RecordService : IRecordService
{
    private readonly IRecordsRepository _recordsRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly ITimeSlotsRepository _timeSlotsRepository;
    private readonly IMapper _mapper;

    public RecordService(
        IRecordsRepository recordsRepository,
        IPatientRepository patientRepository,
        IDoctorsRepository doctorsRepository,
        ITimeSlotsRepository timeSlotsRepository,
        IMapper mapper
        )
    {
        _recordsRepository = recordsRepository;
        _patientRepository = patientRepository;
        _mapper = mapper;
        _timeSlotsRepository = timeSlotsRepository;
        _doctorsRepository = doctorsRepository;
    }

    public async Task<IEnumerable<GetRecordDto>> GetPatientCardAsync(int patientId)
    {
        if (patientId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(patientId));
        }

        var patient = await _patientRepository.GetByIdAsync(patientId)!
                      ?? throw new EntityNotFoundException($"No Patient with Id '{patientId}'");

        return _mapper.Map<IEnumerable<GetRecordDto>>(patient.Records);
    }

    public async Task<IEnumerable<GetRecordDto>> GetAllRecordsAsync()
    {
        var records = await _recordsRepository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<GetRecordDto>>(records);
    }

    public async Task<GetRecordDto> GetRecordByIdAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var record = await _recordsRepository.GetByIdAsync(id)!
                      ?? throw new EntityNotFoundException($"No Record with Id '{id}'");

        return _mapper.Map<GetRecordDto>(record);
    }

    public async Task<GetRecordDto> AddNewRecordAsync(CreateRecordDto createRecordDto)
    {
        await ValidateForeignKeys(createRecordDto.DoctorId, createRecordDto.PatientId, createRecordDto.TimeSlotId);

        var record = _mapper.Map<Record>(createRecordDto);

        var createdRecord = await _recordsRepository.AddAsync(record);

        return _mapper.Map<GetRecordDto>(createdRecord);
    }

    public async Task<GetRecordDto> UpdateRecordAsync(UpdateRecordDto updateRecordDto)
    {
        if (await _recordsRepository.GetByIdAsync(updateRecordDto.Id)! is null)
        {
            throw new EntityNotFoundException($"No Record with Id '{updateRecordDto.Id}'");
        }

        await ValidateForeignKeys(updateRecordDto.DoctorId, updateRecordDto.PatientId, updateRecordDto.TimeSlotId);
        
        var recordToUpdate = _mapper.Map<Record>(updateRecordDto);

        var updatedRecord = await _recordsRepository.UpdateAsync(updateRecordDto.Id, recordToUpdate);
        
        return _mapper.Map<GetRecordDto>(updatedRecord);
    }

    public async Task<GetRecordDto> DeleteRecordAsync(int id)
    {
        var deletedRecord = await _recordsRepository.DeleteAsync(id)!
                            ?? throw new EntityNotFoundException($"No Record with Id '{id}'");

        return _mapper.Map<GetRecordDto>(deletedRecord);
    }

    private async Task ValidateForeignKeys(int doctorId, int patientId, int timeSlotId)
    {
        if (await _patientRepository.GetByIdAsync(patientId)! is null)
        {
            throw new EntityNotFoundException($"No Patient with Id '{patientId}'");
        }

        if (await _doctorsRepository.GetByIdAsync(doctorId)! is null)
        {
            throw new EntityNotFoundException($"No Doctor with Id '{doctorId}'");
        }
        
        if (await _timeSlotsRepository.GetByIdAsync(timeSlotId)! is null)
        {
            throw new EntityNotFoundException($"No Time Slot with Id '{timeSlotId}'");
        }
    }
}