using HospitalRegistrar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalRegistrar.Persistence.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Doctor> Doctors { get; set; }
    
    public virtual DbSet<Patient> Patients { get; set; }
    
    public virtual DbSet<Record> Records { get; set; }
    
    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}