using AutoMapper;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.DoctorFeatures;
using Moq;

namespace HospitalRegistrar.Core.Tests;

public class DoctorsServiceTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IDoctorsRepository> _doctorRepositoryMock = new();
    private DoctorsService _doctorsService = null!;

    [Fact]
    public async Task AddAsync_ReturnAddedDoctor()
    {
        // Arrange
        var createDoctorDto = new CreateDoctorDto { Name = "Name", Position = "Nurse" };
        var doctor = new Doctor { Id = 1, Name = "Name", Position = "Nurse" };
        var getDoctorDto = new GetDoctorDto { Id = 1, Name = "Name", Position = "Nurse" };

        _doctorRepositoryMock.Setup(repo => repo.AddAsync(doctor))
            .ReturnsAsync(doctor);
        _mapperMock.Setup(mapper => mapper.Map<Doctor>(createDoctorDto))
            .Returns(doctor);
        _mapperMock.Setup(mapper => mapper.Map<GetDoctorDto>(doctor))
            .Returns(getDoctorDto);

        _doctorsService = new DoctorsService(_doctorRepositoryMock.Object, _mapperMock.Object );

        // Act
        var addedDoctor = await _doctorsService.AddNewDoctorAsync(createDoctorDto);

        // Assert
        Assert.NotNull(addedDoctor);
        Assert.Equal(createDoctorDto.Name, addedDoctor.Name);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnIEnumerableOfDoctors()
    {
        // Arrange
        var doctors = new List<Doctor>
        {
            new() { Id = 1, Name = "Name", Position = "Nurse" },
            new() { Id = 2, Name = "Name1", Position = "Nurse1" },
        };
        var doctorsDtos = new List<GetDoctorDto>
        {
            new() { Id = 1, Name = "Name", Position = "Nurse" },
            new() { Id = 2, Name = "Name1", Position = "Nurse1" },
        };

        _doctorRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(doctors);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetDoctorDto>>(doctors))
            .Returns(doctorsDtos);

        _doctorsService = new DoctorsService(_doctorRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedDoctorsDtos = (await _doctorsService.GetAllDoctorsAsync()).ToList();

        // Assert
        Assert.NotNull(returnedDoctorsDtos);
        Assert.IsAssignableFrom<IEnumerable<GetDoctorDto>>(returnedDoctorsDtos);
        Assert.Equal(doctors.Count, returnedDoctorsDtos.Count);
        Assert.Equal(doctors[0].Name, returnedDoctorsDtos[0].Name);
        Assert.Equal(doctors[1].Position, returnedDoctorsDtos[1].Position);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnDoctor()
    {
        // Arrange
        const int id = 1;
        var doctor = new Doctor {  Id = id, Name = "Name", Position = "Nurse" };
        var doctorDto = new GetDoctorDto { Id = id, Name = "Name", Position = "Nurse" };

        _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(doctor);
        _mapperMock.Setup(mapper => mapper.Map<GetDoctorDto>(doctor))
            .Returns(doctorDto);

        _doctorsService = new DoctorsService(_doctorRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedDoctorDto = await _doctorsService.GetDoctorByIdAsync(id);

        // Assert
        Assert.NotNull(returnedDoctorDto);
        Assert.Equal(doctor.Name, returnedDoctorDto.Name);
        Assert.Equal(doctor.Id, returnedDoctorDto.Id);
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnUpdatedDoctor()
    {
        // Arrange
        const int id = 1;
        var doctorToUpdateDto = new UpdateDoctorDto {  Id = id, Name = "New Name", Position = "New Nurse" };
        var doctor = new Doctor {  Id = id, Name = "Name", Position = "Nurse" };
        var doctorDto = new GetDoctorDto { Id = id, Name = "New Name", Position = "New Nurse" };

        _doctorRepositoryMock.Setup(repo => repo.UpdateAsync(id, doctor))
            .ReturnsAsync(doctor);
        _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(doctor);
        _mapperMock.Setup(mapper => mapper.Map<Doctor>(doctorToUpdateDto))
            .Returns(doctor);
        _mapperMock.Setup(mapper => mapper.Map<GetDoctorDto>(doctor))
            .Returns(doctorDto);

        _doctorsService = new DoctorsService(_doctorRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedDoctorDto = await _doctorsService.UpdateDoctorAsync(doctorToUpdateDto);

        // Assert
        Assert.NotNull(returnedDoctorDto);
        Assert.Equal(doctorToUpdateDto.Name, returnedDoctorDto.Name);
        Assert.Equal(doctorToUpdateDto.Id, returnedDoctorDto.Id);
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnUpdatedDoctor()
    {
        // Arrange
        const int id = 1;
        var doctor = new Doctor {  Id = id, Name = "Name", Position = "Nurse" };
        var doctorDto = new GetDoctorDto { Id = id, Name = "Name", Position = "Nurse" };

        _doctorRepositoryMock.Setup(repo => repo.DeleteAsync(id)!)
            .ReturnsAsync(doctor);
        _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(doctor);
        _mapperMock.Setup(mapper => mapper.Map<GetDoctorDto>(doctor))
            .Returns(doctorDto);

        _doctorsService = new DoctorsService(_doctorRepositoryMock.Object, _mapperMock.Object );

        // Act
        var returnedDoctorDto = await _doctorsService.DeleteDoctorAsync(id);

        // Assert
        Assert.NotNull(returnedDoctorDto);
        Assert.Equal(doctor.Name, returnedDoctorDto.Name);
        Assert.Equal(doctorDto.Id, returnedDoctorDto.Id);
    }
}