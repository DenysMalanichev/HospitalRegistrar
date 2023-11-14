using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.PatientFeatures;
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
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetPatientByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewPatientAsync(CreatePatientDto createPatientDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdatePatientAsync(UpdatePatientDto updatePatientDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeletePatientAsync(int id)
    {
        throw new NotImplementedException();
    }
}