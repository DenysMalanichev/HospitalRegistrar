using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.DoctorFeatures;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrar.Controllers;

[Route("[controller]")]
public class DoctorsController : ControllerBase
{
    private readonly IDoctorsService _doctorsService;

    public DoctorsController(IDoctorsService doctorsService)
    {
        _doctorsService = doctorsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDoctorsAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetDoctorByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewDoctorAsync(CreateDoctorDto createDoctorDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateDoctorAsync(UpdateDoctorDto updateDoctorDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("delete/{id:int}")]
    public async Task<IActionResult> DeleteDoctorAsync(int id)
    {
        throw new NotImplementedException();
    }
}