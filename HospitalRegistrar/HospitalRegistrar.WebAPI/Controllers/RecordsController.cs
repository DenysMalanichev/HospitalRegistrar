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
        var records = await _recordService.GetPatientCardAsync(patientId);

        return Ok(records);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRecordByIdAsync(int id)
    {
        var records = await _recordService.GetRecordByIdAsync(id);

        return Ok(records);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRecordsAsync()
    {
        var records = await _recordService.GetAllRecordsAsync();

        return Ok(records);
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewRecordAsync([FromBody] CreateRecordDto createRecordDto)
    {
        var record = await _recordService.AddNewRecordAsync(createRecordDto);

        return Ok(record);
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateRecordAsync([FromBody] UpdateRecordDto updateRecordDto)
    {
        var updatedRecord = await _recordService.UpdateRecordAsync(updateRecordDto);

        return Ok(updatedRecord);
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteRecordAsync(int id)
    {
        var deletedRecord = await _recordService.DeleteRecordAsync(id);

        return Ok(deletedRecord);
    }
}