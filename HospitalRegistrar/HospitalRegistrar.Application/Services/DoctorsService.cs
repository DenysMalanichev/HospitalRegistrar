using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.DoctorFeatures;

namespace HospitalRegistrar.Application.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IMapper _mapper;

    public DoctorsService(IDoctorsRepository doctorsRepository, IMapper mapper)
    {
        _doctorsRepository = doctorsRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetDoctorDto>> GetAllDoctorsAsync()
    {
        var doctors = await _doctorsRepository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<GetDoctorDto>>(doctors);
    }

    public async Task<GetDoctorDto> GetDoctorByIdAsync(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id));
        }

        var doctor = await _doctorsRepository.GetByIdAsync(id)!
                     ?? throw new EntityNotFoundException($"No Doctor with Id '{id}'");

        return _mapper.Map<GetDoctorDto>(doctor);
    }

    public async Task<GetDoctorDto> AddNewDoctorAsync(CreateDoctorDto createDoctorDto)
    {
        var doctor = _mapper.Map<Doctor>(createDoctorDto);

        var createdDoctor = await _doctorsRepository.AddAsync(doctor);

        return _mapper.Map<GetDoctorDto>(createdDoctor);
    }

    public async Task<GetDoctorDto> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto)
    {
        if (await _doctorsRepository.GetByIdAsync(updateDoctorDto.Id)! is null)
        {
            throw new EntityNotFoundException($"No Doctor with Id '{updateDoctorDto.Id}'");
        }
        
        var doctorToUpdate = _mapper.Map<Doctor>(updateDoctorDto);

        var updatedDoctor = await _doctorsRepository.UpdateAsync(updateDoctorDto.Id, doctorToUpdate);
        
        return _mapper.Map<GetDoctorDto>(updatedDoctor);
    }

    public async Task<GetDoctorDto> DeleteDoctorAsync(int id)
    {
        var deletedDoctor = await _doctorsRepository.DeleteAsync(id)!
                            ?? throw new EntityNotFoundException($"No Doctor with Id '{id}'");

        return _mapper.Map<GetDoctorDto>(deletedDoctor);
    }
}