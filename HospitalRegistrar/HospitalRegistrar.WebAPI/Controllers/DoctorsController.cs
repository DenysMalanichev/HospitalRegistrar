using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.AuthModels;
using HospitalRegistrar.Features.DoctorFeatures;
using Microsoft.AspNetCore.Authorization;
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
        var doctors = await _doctorsService.GetAllDoctorsAsync();

        return Ok(doctors);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetDoctorByIdAsync(int id)
    {
        var doctor = await _doctorsService.GetDoctorByIdAsync(id);

        return Ok(doctor);
    }
    
    [HttpPost]
    [Route("new")]
    [Authorize(Roles = IdentityRoles.Doctor)]
    public async Task<IActionResult> CreateNewDoctorAsync([FromBody] CreateDoctorDto createDoctorDto)
    {
        var createdDoctor = await _doctorsService.AddNewDoctorAsync(createDoctorDto);

        return Ok(createdDoctor);
    }

    [HttpPost]
    [Route("add-timeslot/{doctorId:int}/{timeSlotId:int}")]
    public async Task<IActionResult> AssociateDoctorAndTimeSlot(int doctorId, int timeSlotId)
    {
        var doctorWithTimeSlot = await _doctorsService.AssociateWithTimeSlot(doctorId, timeSlotId);
        
        return Ok(doctorWithTimeSlot);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateDoctorAsync([FromBody] UpdateDoctorDto updateDoctorDto)
    {
        var updatedDoctor = await _doctorsService.UpdateDoctorAsync(updateDoctorDto);

        return Ok(updatedDoctor);
    }
    
    [HttpDelete]
    [Route("delete/{id:int}")]
    public async Task<IActionResult> DeleteDoctorAsync(int id)
    {
        var deletedDoctor = await _doctorsService.DeleteDoctorAsync(id);

        return Ok(deletedDoctor);
    }
}