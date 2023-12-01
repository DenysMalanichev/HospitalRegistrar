using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;
using HospitalRegistrar.Persistence.Context;
using HospitalRegistrar.Persistence.Repositories;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Record = HospitalRegistrar.Domain.Entities.Record;

namespace HospitalRegistrar.Persistence.Tests;

public class TimeSlotRepositoryTests
{
    private readonly ITimeSlotsRepository _timeSlotsRepository;
    private readonly DataContext _dataContext;

    public TimeSlotRepositoryTests()
    {
        var options = CreateNewContextOptions();
        _dataContext = new DataContext(options);
        _timeSlotsRepository = new TimeSlotRepository(_dataContext);
    }
    
    [Fact]
    public async Task GetAvailableTimeSlotsByPredicateAsync_ReturnCorrespondingEntity_IfExists()
    {
        // Arrange
        var timeSlots = new List<TimeSlot>
        {
            new()
            { 
                Id = 1,
                Doctors = new List<Doctor> {new Doctor { Id = 1, Name = "test", Position = "doctor1"}},
                Records = new List<Record> {new Record { Id = 1, Diagnosis = string.Empty}},
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
            new()
            { 
                Id = 2,
                Doctors = new List<Doctor> {new Doctor { Id = 2, Name = "test2", Position = "doctor2" }},
                Records = new List<Record> {new Record { Id = 2, Diagnosis = string.Empty }},
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
            new()
            { 
                Id = 3,
                Doctors = new List<Doctor> {new Doctor { Id = 3, Name = "test3", Position = "doctor" }},
                Records = new List<Record> {new Record { Id = 3, Diagnosis = string.Empty }},
                TimeBegin = DateTime.Today.AddDays(1),
                TimeEnd = DateTime.Today,
            },
        };

        await _dataContext.TimeSlots.AddRangeAsync(timeSlots);
        await _dataContext.SaveChangesAsync();
        
        var predicate = PredicateBuilder.New<TimeSlot>(false);
        
        predicate = predicate.And(new DoctorSpecialtySpecification("doctor").Criteria);
        predicate = predicate.And(new DoctorNameSpecification("test3").Criteria);
        predicate = predicate.And(new TimeSlotDateSpecification(DateTime.Today.AddDays(1)).Criteria);

        // Act
        var foundTimeSlots = (await _timeSlotsRepository.GetAvailableTimeSlotsByPredicateAsync(predicate)).ToList();

        // Assert
        Assert.NotNull(foundTimeSlots);
        Assert.Equal(3, foundTimeSlots[0].Id);
        Assert.Equal(DateTime.Today.AddDays(1), foundTimeSlots[0].TimeBegin);
    }
    
    private static DbContextOptions<DataContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
}