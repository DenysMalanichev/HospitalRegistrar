using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Persistence.Context;
using HospitalRegistrar.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HospitalRegistrar.Persistence.Tests;

public class PatientRepositoryTests
{
    private readonly IPatientRepository _patientRepository;
    private readonly DataContext _dataContext;

    public PatientRepositoryTests()
    {
        var options = CreateNewContextOptions();
        _dataContext = new DataContext(options);
        _patientRepository = new PatientRepository(_dataContext);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnCorrespondingEntity_IfExists()
    {
        // Arrange
        const int id = 1;
        var patient = new Patient { Id = id };

        await _dataContext.Patients.AddAsync(patient);
        await _dataContext.SaveChangesAsync();

        // Act
        var foundPatient = await _patientRepository.GetByIdAsync(id)!;

        // Assert
        Assert.NotNull(foundPatient);
        Assert.Equal(id, foundPatient.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnNull_IfEntityDoesntExists()
    {
        // Arrange & Act
        var foundGenre = await _patientRepository.GetByIdAsync(1)!;

        // Assert
        Assert.Null(foundGenre);
    }

    [Fact]
    public async Task GetAllAsync_ReturnEnumerable_IfEntitiesExists()
    {
        // Arrange
        var patients = new List<Patient>
        {
            new() { Id = 1 },
            new() { Id = 2 },
        };

        await _dataContext.AddRangeAsync(patients);
        await _dataContext.SaveChangesAsync();

        // Act
        var foundPatients = (await _patientRepository.GetAllAsync()).ToList();

        // Assert
        Assert.NotNull(foundPatients);
        Assert.Equal(patients.Count, foundPatients.Count);
    }

    [Fact]
    public async Task GetAllAsync_ReturnEmptyEnumerable_IfEntitiesDontExist()
    {
        // Arrange & Act
        var foundPatients = (await _patientRepository.GetAllAsync()).ToList();

        // Assert
        Assert.NotNull(foundPatients);
        Assert.IsAssignableFrom<IEnumerable<Patient>>(foundPatients);
        Assert.Empty(foundPatients);
    }

    [Fact]
    public async Task AddAsync_ReturnAddedPatient()
    {
        // Arrange
        var patient = new Patient { Age = 20 };

        // Act
        var addedPatient = await _patientRepository.AddAsync(patient);
        await _dataContext.SaveChangesAsync();

        var genreFromDb = await _dataContext.Patients.FirstOrDefaultAsync(g => g.Id == patient.Id);

        // Assert
        Assert.NotNull(addedPatient);
        Assert.NotNull(genreFromDb);
        Assert.Equal(genreFromDb.Age, addedPatient.Age);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsUpdatedPatient()
    {
        // Arrange
        var newPatient = new Patient { Age = 20 };

        // Act
        var patient = (await _dataContext.AddAsync(new Patient { Age = 21 })).Entity;
        await _dataContext.SaveChangesAsync();

        var updatedPatient = await _patientRepository.UpdateAsync(patient.Id, newPatient);
        await _dataContext.SaveChangesAsync();

        // Assert
        Assert.NotNull(updatedPatient);
        Assert.Equal(updatedPatient.Age, newPatient.Age);
    }

    [Fact]
    public async Task DeleteAsync_ReturnDeletedPatient_IfExists()
    {
        // Arrange & Act
        var addedPatient = (await _dataContext.AddAsync(new Patient { Age = 20 })).Entity;
        await _dataContext.SaveChangesAsync();

        var deletedPatient = await _patientRepository.DeleteAsync(addedPatient.Id)!;
        await _dataContext.SaveChangesAsync();

        // Assert
        Assert.NotNull(deletedPatient);
        Assert.Equal(deletedPatient.Age, addedPatient.Age);
        Assert.Null(await _dataContext.Patients.FirstOrDefaultAsync(g => g.Id == addedPatient.Id));
    }

    [Fact]
    public async Task DeleteAsync_ReturnNull_IfEntityDoesntExist()
    {
        // Arrange & Act
        var deletedGPatient = await _patientRepository.DeleteAsync(100)!;
        await _dataContext.SaveChangesAsync();

        // Assert
        Assert.Null(deletedGPatient);
    }

    private static DbContextOptions<DataContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
}