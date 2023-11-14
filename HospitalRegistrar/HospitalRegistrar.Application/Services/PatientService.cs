using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.PatientFeatures;

namespace HospitalRegistrar.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public Task<IEnumerable<GetPatientDto>> GetAllPatientsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetPatientDto> GetPatientByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GetPatientDto> AddNewPatientAsync(CreatePatientDto createPatientDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetPatientDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetPatientDto> DeletePatientAsync(int id)
    {
        throw new NotImplementedException();
    }
}