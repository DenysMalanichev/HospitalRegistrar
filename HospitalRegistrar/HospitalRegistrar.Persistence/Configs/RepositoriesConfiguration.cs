using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.AuthModels;
using HospitalRegistrar.Persistence.Context;
using HospitalRegistrar.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalRegistrar.Persistence.Configs;

public static class RepositoriesConfiguration
{
    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IDoctorsRepository, DoctorRepository>();
        service.AddScoped<IPatientRepository, PatientRepository>();
        service.AddScoped<IRecordsRepository, RecordRepository>();
        service.AddScoped<ITimeSlotsRepository, TimeSlotRepository>();
        service.AddScoped<IQueryingRepository<TimeSlot>, TimeSlotRepository>();
    }
}