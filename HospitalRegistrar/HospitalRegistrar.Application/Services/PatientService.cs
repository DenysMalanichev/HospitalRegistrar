using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.PatientFeatures;

namespace HospitalRegistrar.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetPatientDto>> GetAllPatientsAsync()
    {
        var patients = await _patientRepository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<GetPatientDto>>(patients);
    }

    public async Task<GetPatientDto> GetPatientByIdAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var patient = await _patientRepository.GetByIdAsync(id)!
                     ?? throw new EntityNotFoundException($"No Patient with Id '{id}'");

        return _mapper.Map<GetPatientDto>(patient);
    }

    public async Task<GetPatientDto> AddNewPatientAsync(CreatePatientDto createPatientDto)
    {
        var patient = _mapper.Map<Patient>(createPatientDto);

        var createdPatient = await _patientRepository.AddAsync(patient);

        return _mapper.Map<GetPatientDto>(createdPatient);
    }

    public async Task<GetPatientDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto)
    {
        if (await _patientRepository.GetByIdAsync(updatePatientDto.Id)! is null)
        {
            throw new EntityNotFoundException($"No Patient with Id '{updatePatientDto.Id}'");
        }
        
        var patientToUpdate = _mapper.Map<Patient>(updatePatientDto);

        var updatedDoctor = await _patientRepository.UpdateAsync(updatePatientDto.Id, patientToUpdate);
        
        return _mapper.Map<GetPatientDto>(updatedDoctor);
    }

    public async Task<GetPatientDto> DeletePatientAsync(int id)
    {
        var deletedDoctor = await _patientRepository.DeleteAsync(id)!
                            ?? throw new EntityNotFoundException($"No Patient with Id '{id}'");

        return _mapper.Map<GetPatientDto>(deletedDoctor);
    }
}