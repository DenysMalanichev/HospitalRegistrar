using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.AuthModels;
using HospitalRegistrar.Features.PatientFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrar.Controllers;

[Route("[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatientsAsync()
    {
        var patients = await _patientService.GetAllPatientsAsync();

        return Ok(patients);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetPatientByIdAsync(int id)
    {
        var patient = await _patientService.GetPatientByIdAsync(id);

        return Ok(patient);
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewPatientAsync([FromBody] CreatePatientDto createPatientDto)
    {
        var createdPatient = await _patientService.AddNewPatientAsync(createPatientDto);

        return Ok(createdPatient);
    }
    
    [HttpPut]
    [Route("update")]
    [Authorize(Roles = IdentityRoles.Patient)]
    public async Task<IActionResult> UpdatePatientAsync([FromBody] UpdatePatientDto updatePatientDto)
    {
        var updatedPatient = await _patientService.UpdatePatientAsync(updatePatientDto);

        return Ok(updatedPatient);
    }
    
    [HttpDelete]
    [Route("delete/{id:int}")]
    [Authorize(Roles = IdentityRoles.Patient)]
    public async Task<IActionResult> DeletePatientAsync(int id)
    {
        var deletedPatient = await _patientService.DeletePatientAsync(id);

        return Ok(deletedPatient);
    }
}