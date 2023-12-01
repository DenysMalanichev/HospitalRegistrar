using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Features.TimeSlotFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;
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
    [Route("available")]
    public async Task<IActionResult> GetTimeSlotsBySpecificationsAsync([FromQuery] AvailableTimeSlotsByCriteriaRequestDto criteriaRequestDto)
    {
        var timeSlots = await _timeSlotService.GetTimeSlotsBySpecificationsAsync(criteriaRequestDto);

        return Ok(timeSlots);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllTimeSlotsAsync()
    {
        var timeSlots = await _timeSlotService.GetAllTimeSlotsAsync();

        return Ok(timeSlots);
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetTimeSlotByIdAsync(int id)
    {
        var timeSlot = await _timeSlotService.GetTimeSlotByIdAsync(id);

        return Ok(timeSlot);
    }
    
    [HttpPost]
    [Route("new")]
    public async Task<IActionResult> CreateNewTimeSlotAsync([FromBody] CreateTimeSlotDto createTimeSlotDto)
    {
        var timeSlot = await _timeSlotService.AddNewTimeSlotAsync(createTimeSlotDto);

        return Ok(timeSlot);
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> UpdateTimeSlotAsync([FromBody] UpdateTimeSlotDto updateTimeSlotDto)
    {
        var timeSlot = await _timeSlotService.UpdateTimeSlotAsync(updateTimeSlotDto);

        return Ok(timeSlot);
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteTimeSlotAsync(int id)
    {
        var timeSlots = await _timeSlotService.DeleteTimeSlotAsync(id);

        return Ok(timeSlots);
    }
}