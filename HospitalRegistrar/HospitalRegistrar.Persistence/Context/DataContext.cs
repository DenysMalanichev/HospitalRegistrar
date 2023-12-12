using HospitalRegistrar.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HospitalRegistrar.Persistence.Context;

public class DataContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Doctor> Doctors { get; set; }
    
    public virtual DbSet<Patient> Patients { get; set; }
    
    public virtual DbSet<Record> Records { get; set; }
    
    public virtual DbSet<TimeSlot> TimeSlots { get; set; }
    
    public virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>()
            .Navigation(od => od.TimeSlots)
            .AutoInclude();
        
        modelBuilder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne(u => u.Doctor)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.User)
            .WithOne(u => u.Patient)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Custom configurations for Identity entities with int key
        modelBuilder.Entity<IdentityUserLogin<int>>(i =>
        {
            i.HasKey(x => new { x.ProviderKey, x.LoginProvider });
        });

        modelBuilder.Entity<IdentityUserRole<int>>(i =>
        {
            i.HasKey(x => new { x.RoleId, x.UserId });
        });

        modelBuilder.Entity<IdentityUserToken<int>>(i =>
        {
            i.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        });
    }
}