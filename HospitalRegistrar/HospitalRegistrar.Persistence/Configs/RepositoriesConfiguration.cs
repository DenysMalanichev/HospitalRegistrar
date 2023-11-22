using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalRegistrar.Persistence.Configs;

public static class RepositoriesConfiguration
{
    public static void AddRepositories(this IServiceCollection service)
    {
        service.AddScoped<IDoctorsRepository, DoctorRepository>();
    }
}