using HospitalRegistrar.Features.RecordFeatures;

namespace HospitalRegistrar.Application.Interfaces.Services;

public interface IRecordService
{
    Task<IEnumerable<GetRecordDto>> GetPatientCardAsync(int patientId);
    
    Task<IEnumerable<GetRecordDto>> GetAllRecordsAsync();
    
    Task<GetRecordDto> GetRecordByIdAsync(int id);
    
    Task<GetRecordDto> AddNewRecordAsync(CreateRecordDto createRecordDto);
    
    Task<GetRecordDto> UpdateRecordAsync(UpdateRecordDto updateRecordDto);

    Task<GetRecordDto> DeleteRecordAsync(int id);
}