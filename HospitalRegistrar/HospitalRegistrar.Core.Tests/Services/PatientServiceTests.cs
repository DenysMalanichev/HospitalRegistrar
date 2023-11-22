using AutoMapper;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.DoctorFeatures;
using HospitalRegistrar.Features.PatientFeatures;
using Moq;

namespace HospitalRegistrar.Core.Tests.Services;

public class PatientServiceTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IPatientRepository> _patientRepositoryMock = new();
    private PatientService _patientService = null!;

    [Fact]
    public async Task AddAsync_ReturnAddedPatient()
    {
        // Arrange
        var createPatientDto = new CreatePatientDto { Name = "Name", Age = 20 };
        var patient = new Patient { Id = 1, Name = "Name", Age = 20 };
        var getDoctorDto = new GetPatientDto { Id = 1, Name = "Name", Age = 20 };

        _patientRepositoryMock.Setup(repo => repo.AddAsync(patient))
            .ReturnsAsync(patient);
        _mapperMock.Setup(mapper => mapper.Map<Patient>(createPatientDto))
            .Returns(patient);
        _mapperMock.Setup(mapper => mapper.Map<GetPatientDto>(patient))
            .Returns(getDoctorDto);

        _patientService = new PatientService(_patientRepositoryMock.Object, _mapperMock.Object );

        // Act
        var addedPatient = await _patientService.AddNewPatientAsync(createPatientDto);

        // Assert
        Assert.NotNull(addedPatient);
        Assert.Equal(createPatientDto.Name, addedPatient.Name);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnIEnumerableOfPatients()
    {
        // Arrange
        var patients = new List<Patient>
        {
            new() { Id = 1, Name = "Name", Age = 20 },
            new() { Id = 2, Name = "Name1", Age = 80 },
        };
        var patientDtos = new List<GetPatientDto>
        {
            new() { Id = 1, Name = "Name", Age = 20 },
            new() { Id = 2, Name = "Name1", Age = 80 },
        };

        _patientRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(patients);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetPatientDto>>(patients))
            .Returns(patientDtos);

        _patientService = new PatientService(_patientRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedPatientsDtos = (await _patientService.GetAllPatientsAsync()).ToList();

        // Assert
        Assert.NotNull(returnedPatientsDtos);
        Assert.IsAssignableFrom<IEnumerable<GetPatientDto>>(returnedPatientsDtos);
        Assert.Equal(patients.Count, returnedPatientsDtos.Count);
        Assert.Equal(patients[0].Name, returnedPatientsDtos[0].Name);
        Assert.Equal(patients[1].Age, returnedPatientsDtos[1].Age);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnPatient()
    {
        // Arrange
        const int id = 1;
        var patient = new Patient {  Id = id, Name = "Name", Age = 20 };
        var patientDto = new GetPatientDto { Id = id, Name = "Name", Age = 20 };

        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(patient);
        _mapperMock.Setup(mapper => mapper.Map<GetPatientDto>(patient))
            .Returns(patientDto);

        _patientService = new PatientService(_patientRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedPatientDto = await _patientService.GetPatientByIdAsync(id);

        // Assert
        Assert.NotNull(returnedPatientDto);
        Assert.Equal(patient.Name, returnedPatientDto.Name);
        Assert.Equal(patient.Id, returnedPatientDto.Id);
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnUpdatedPatient()
    {
        // Arrange
        const int id = 1;
        var updatePatientDto = new UpdatePatientDto {  Id = id, Name = "New Name", Age = 21 };
        var patient = new Patient {  Id = id, Name = "Name", Age = 20 };
        var patientDto = new GetPatientDto { Id = id, Name = "New Name", Age = 21 };

        _patientRepositoryMock.Setup(repo => repo.UpdateAsync(id, patient))
            .ReturnsAsync(patient);
        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(patient);
        _mapperMock.Setup(mapper => mapper.Map<Patient>(updatePatientDto))
            .Returns(patient);
        _mapperMock.Setup(mapper => mapper.Map<GetPatientDto>(patient))
            .Returns(patientDto);

        _patientService = new PatientService(_patientRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedPatientDto = await _patientService.UpdatePatientAsync(updatePatientDto);

        // Assert
        Assert.NotNull(returnedPatientDto);
        Assert.Equal(updatePatientDto.Name, returnedPatientDto.Name);
        Assert.Equal(updatePatientDto.Id, returnedPatientDto.Id);
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnUpdatedPatient()
    {
        // Arrange
        const int id = 1;
        var patient = new Patient {  Id = id, Name = "Name", Age = 20 };
        var patientDto = new GetPatientDto { Id = id, Name = "Name", Age = 20 };

        _patientRepositoryMock.Setup(repo => repo.DeleteAsync(id)!)
            .ReturnsAsync(patient);
        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(patient);
        _mapperMock.Setup(mapper => mapper.Map<GetPatientDto>(patient))
            .Returns(patientDto);

        _patientService = new PatientService(_patientRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedDoctorDto = await _patientService.DeletePatientAsync(id);

        // Assert
        Assert.NotNull(returnedDoctorDto);
        Assert.Equal(patient.Name, returnedDoctorDto.Name);
        Assert.Equal(patientDto.Id, returnedDoctorDto.Id);
    }
}