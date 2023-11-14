using HospitalRegistrar.Features.PatientFeatures;
using HospitalRegistrar.Features.RecordFeatures;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface IPatientService
{
    Task<IEnumerable<GetPatientDto>> GetAllPatientsAsync();
    
    Task<GetPatientDto> GetPatientByIdAsync(int id);
    
    Task<GetPatientDto> AddNewPatientAsync(CreatePatientDto createPatientDto);
    
    Task<GetPatientDto> UpdatePatientAsync(UpdatePatientDto updatePatientDto);

    Task<GetPatientDto> DeletePatientAsync(int id);
}