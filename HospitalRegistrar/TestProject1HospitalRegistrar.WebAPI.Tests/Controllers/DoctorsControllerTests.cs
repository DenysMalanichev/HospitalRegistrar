using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Controllers;
using HospitalRegistrar.Features.DoctorFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject1HospitalRegistrar.WebAPI.Tests.Controllers;

public class DoctorsControllerTests
{
    private readonly Mock<IDoctorsService> _doctorServiceMock = new();
    private DoctorsController _doctorsController = null!;
    
    [Fact]
    public async Task GetAllDoctorsAsync_ReturnArrayOfDoctors()
    {
        // Arrange
        var doctors = new List<GetDoctorDto>
        {
            new()
            {
                Id = 1,
                Name = "Test Name",
                Position = "Pos",
            },
            new()
            {
                Id = 2,
                Name = "Test Name2",
                Position = "Pos2",
            },
        };

        _doctorServiceMock.Setup(ds => ds.GetAllDoctorsAsync())
            .ReturnsAsync(doctors);
        _doctorsController = new DoctorsController(_doctorServiceMock.Object);

        // Act
        var returnedResult = await _doctorsController.GetAllDoctorsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(returnedResult);
        var returnValue = Assert.IsType<List<GetDoctorDto>>(okResult.Value);
        Assert.Equal(doctors.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetDoctorByIdAsync_ReturnDoctor()
    {
        // Arrange
        const int id = 1;
        
        var doctor = new GetDoctorDto
        {
            Id = id,
            Name = "Test Name",
            Position = "Pos",
        };

        _doctorServiceMock.Setup(ds => ds.GetDoctorByIdAsync(id))
            .ReturnsAsync(doctor);
        _doctorsController = new DoctorsController(_doctorServiceMock.Object);

        // Act
        var returnedResult = await _doctorsController.GetDoctorByIdAsync(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(returnedResult);
        var returnValue = Assert.IsType<GetDoctorDto>(okResult.Value);
        Assert.Equal(doctor.Id, returnValue.Id);
    }
    
    [Fact]
    public async Task CreateNewDoctorAsync_ReturnCreatedDoctor()
    {
        // Arrange
        const int id = 1;
        
        var doctor = new GetDoctorDto
        {
            Id = id,
            Name = "Test Name",
            Position = "Pos",
        };

        _doctorServiceMock.Setup(ds => ds.GetDoctorByIdAsync(id))
            .ReturnsAsync(doctor);
        _doctorsController = new DoctorsController(_doctorServiceMock.Object);

        // Act
        var returnedResult = await _doctorsController.GetDoctorByIdAsync(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(returnedResult);
        var returnValue = Assert.IsType<GetDoctorDto>(okResult.Value);
        Assert.Equal(doctor.Id, returnValue.Id);
    }
}