using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Application.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.AuthModels;
using HospitalRegistrar.Features.TimeSlotFeatures;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalRegistrar.Application.Configs;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IDoctorsService, DoctorsService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IRecordService, RecordService>();
        services.AddScoped<ITimeSlotService, TimeSlotService>();
        services.AddScoped<IDataQueryingService<TimeSlot, GetTimeSlotDto>, DataQueryService<TimeSlot, GetTimeSlotDto>>();
    }
}