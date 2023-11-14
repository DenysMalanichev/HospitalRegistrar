using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.RecordFeatures;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrar.Controllers;

[Route("[controller]")]
public class RecordsController : ControllerBase
{
    private readonly IRecordService _recordService;

    public RecordsController(IRecordService recordService)
    {
        _recordService = recordService;
    }

    [HttpGet]
    [Route("/card/{patientId:int}")]
    public async Task<IActionResult> GetPatientCardAsync(int patientId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRecordByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRecordsAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewRecordAsync(CreateRecordDto createRecordDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateRecordAsync(UpdateRecordDto updateRecordDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteRecordAsync(int id)
    {
        throw new NotImplementedException();
    }
}