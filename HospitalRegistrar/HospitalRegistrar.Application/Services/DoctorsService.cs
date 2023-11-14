using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.DoctorFeatures;

namespace HospitalRegistrar.Application.Services;

public class DoctorsService : IDoctorsService
{
    private readonly IDoctorsRepository _doctorsRepository;

    public DoctorsService(IDoctorsRepository doctorsRepository)
    {
        _doctorsRepository = doctorsRepository;
    }

    public Task<IEnumerable<GetDoctorDto>> GetAllDoctorsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetDoctorDto> GetDoctorByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GetDoctorDto> AddNewDoctorAsync(CreateDoctorDto createDoctorDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetDoctorDto> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetDoctorDto> DeleteDoctorAsync(int id)
    {
        throw new NotImplementedException();
    }
}