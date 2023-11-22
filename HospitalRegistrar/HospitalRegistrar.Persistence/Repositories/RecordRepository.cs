using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Persistence.Context;

namespace HospitalRegistrar.Persistence.Repositories;

public class RecordRepository : GenericRepository<Record>, IRecordsRepository
{
    public RecordRepository(DataContext context) : base(context)
    {
    }
}