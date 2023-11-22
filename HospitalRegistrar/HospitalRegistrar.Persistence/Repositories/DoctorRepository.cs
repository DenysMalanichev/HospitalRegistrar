using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Persistence.Context;

namespace HospitalRegistrar.Persistence.Repositories;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorsRepository
{
    public DoctorRepository(DataContext context) : base(context)
    {
    }
}