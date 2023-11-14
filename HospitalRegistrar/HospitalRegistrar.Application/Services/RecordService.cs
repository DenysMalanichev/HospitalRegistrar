using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.RecordFeatures;

namespace HospitalRegistrar.Application.Services;

public class RecordService : IRecordService
{
    private readonly IRecordsRepository _recordsRepository;
    private readonly IPatientService _patientService;

    public RecordService(IRecordsRepository recordsRepository, IPatientService patientService)
    {
        _recordsRepository = recordsRepository;
        _patientService = patientService;
    }

    public Task<IEnumerable<GetRecordDto>> GetPatientCardAsync(int patientId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetRecordDto>> GetAllRecordsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GetRecordDto> GetRecordByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GetRecordDto> AddNewRecordAsync(CreateRecordDto createRecordDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetRecordDto> UpdateRecordAsync(UpdateRecordDto updateRecordDto)
    {
        throw new NotImplementedException();
    }

    public Task<GetRecordDto> DeleteRecordAsync(int id)
    {
        throw new NotImplementedException();
    }
}