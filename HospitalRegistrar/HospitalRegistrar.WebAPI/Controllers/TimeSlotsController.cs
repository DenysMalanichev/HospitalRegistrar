using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.TimeSlotFeatures;
using Microsoft.AspNetCore.Mvc;

namespace HospitalRegistrar.Controllers;

[Route("[controller]")]
public class TimeSlotsController : ControllerBase
{
    private readonly ITimeSlotService _timeSlotService;

    public TimeSlotsController(ITimeSlotService timeSlotService)
    {
        _timeSlotService = timeSlotService;
    }

    [HttpGet]
    [Route("by-speciality/{speciality}")]
    public async Task<IActionResult> GetTimeSlotsByDoctorsSpecialityAsync(string speciality)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("by-doctor/{doctorId:int}")]
    public async Task<IActionResult> GetTimeSlotsByDoctorAsync(int doctorId)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTimeSlotsAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTimeSlotByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewTimeSlotAsync(CreateTimeSlotDto createTimeSlotDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateTimeSlotAsync(UpdateTimeSlotDto updateTimeSlotDto)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteTimeSlotAsync(int id)
    {
        throw new NotImplementedException();
    }
}