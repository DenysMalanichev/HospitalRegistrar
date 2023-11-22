using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalRegistrar.Application.Configs;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IDoctorsService, DoctorsService>();
    }
}