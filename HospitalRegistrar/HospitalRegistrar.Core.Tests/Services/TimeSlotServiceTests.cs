using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Interfaces.Services;
using HospitalRegistrar.Application.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.TimeSlotFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures.Specification;
using LinqKit;
using Moq;

namespace HospitalRegistrar.Core.Tests.Services;

public class TimeSlotServiceTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ITimeSlotsRepository> _timeSlotRepositoryMock = new();
    private readonly Mock<IPatientService> _patientServiceMock = new();
    private readonly Mock<IDoctorsService> _doctorServiceMock = new();
    private TimeSlotService _timeSlotService = null!;

    [Fact]
    public async Task AddAsync_ReturnAddedTimeSlot()
    {
        // Arrange
        var createTimeSlotDto = new CreateTimeSlotDto
        {
            IsVacant = true,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var timeSlot = new TimeSlot
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var getTimeSlotDto = new GetTimeSlotDto
        {
            Id = 1,
            IsVacant = true,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };

        _timeSlotRepositoryMock.Setup(repo => repo.AddAsync(timeSlot))
            .ReturnsAsync(timeSlot);
        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(timeSlot.Id)!)
            .ReturnsAsync(new TimeSlot());
        _mapperMock.Setup(mapper => mapper.Map<TimeSlot>(createTimeSlotDto))
            .Returns(timeSlot);
        _mapperMock.Setup(mapper => mapper.Map<GetTimeSlotDto>(timeSlot))
            .Returns(getTimeSlotDto);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object );

        // Act
        var addedRecord = await _timeSlotService.AddNewTimeSlotAsync(createTimeSlotDto);

        // Assert
        Assert.NotNull(addedRecord);
        Assert.IsType<GetTimeSlotDto>(addedRecord);
        Assert.Equal(createTimeSlotDto.TimeEnd, addedRecord.TimeEnd);
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnIEnumerableOfTimeSlots()
    {
        // Arrange
        var timeSlots = new List<TimeSlot>
        {
            new() 
            {
                Id = 1,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
            new() 
            {
                Id = 2,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
        };
        var getTimeSlotDtos = new List<GetTimeSlotDto>
        {
            new() 
            {
                Id = 1,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
            new() 
            {
                Id = 2,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
        };

        _timeSlotRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(timeSlots);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetTimeSlotDto>>(timeSlots))
            .Returns(getTimeSlotDtos);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object );

        // Act
        var returnedPatientsDtos = (await _timeSlotService.GetAllTimeSlotsAsync()).ToList();

        // Assert
        Assert.NotNull(returnedPatientsDtos);
        Assert.IsAssignableFrom<IEnumerable<GetTimeSlotDto>>(returnedPatientsDtos);
        Assert.Equal(timeSlots.Count, returnedPatientsDtos.Count);
        Assert.Equal(timeSlots[0].Id, returnedPatientsDtos[0].Id);
        Assert.Equal(timeSlots[1].TimeBegin, returnedPatientsDtos[1].TimeBegin);
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnRecord()
    {
        // Arrange
        const int id = 1;
        var timeSlot = new TimeSlot
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var getTimeSlotDto = new GetTimeSlotDto
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };

        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(timeSlot);
        _mapperMock.Setup(mapper => mapper.Map<GetTimeSlotDto>(timeSlot))
            .Returns(getTimeSlotDto);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object );

        // Act
        var returnedPatientDto = await _timeSlotService.GetTimeSlotByIdAsync(id);

        // Assert
        Assert.NotNull(returnedPatientDto);
        Assert.IsType<GetTimeSlotDto>(returnedPatientDto);
        Assert.Equal(timeSlot.TimeBegin, returnedPatientDto.TimeBegin);
        Assert.Equal(timeSlot.Id, returnedPatientDto.Id);
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnUpdatedRecord()
    {
        // Arrange
        const int id = 1;
        var updateTimeSlotDto = new UpdateTimeSlotDto
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var timeSlot = new TimeSlot
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var getTimeSlotDto = new GetTimeSlotDto()
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };

        _timeSlotRepositoryMock.Setup(repo => repo.UpdateAsync(id, timeSlot))
            .ReturnsAsync(timeSlot);
        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(timeSlot);
        _mapperMock.Setup(mapper => mapper.Map<TimeSlot>(updateTimeSlotDto))
            .Returns(timeSlot);
        _mapperMock.Setup(mapper => mapper.Map<GetTimeSlotDto>(timeSlot))
            .Returns(getTimeSlotDto);
        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(timeSlot.Id)!)
            .ReturnsAsync(new TimeSlot());

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object );

        // Act
        var returnedPatientDto = await _timeSlotService.UpdateTimeSlotAsync(updateTimeSlotDto);

        // Assert
        Assert.NotNull(returnedPatientDto);
        Assert.Equal(updateTimeSlotDto.TimeBegin, returnedPatientDto.TimeBegin);
        Assert.Equal(updateTimeSlotDto.Id, returnedPatientDto.Id);
    }
    
    [Fact]
    public async Task UpdateAsync_ThrowsException_IfEntityDoesntExist()
    {
        // Arrange
        const int id = 1;
        var updateTimeSlotDto = new UpdateTimeSlotDto
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var timeSlot = new TimeSlot
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var getTimeSlotDto = new GetTimeSlotDto
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };

        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(timeSlot.Id)!)
            .ReturnsAsync((TimeSlot)null!);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object );

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _timeSlotService.UpdateTimeSlotAsync(updateTimeSlotDto));
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnUpdatedRecord()
    {
        // Arrange
        const int id = 1;
        var timeSlot = new TimeSlot
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };
        var getTimeSlotDto = new GetTimeSlotDto
        {
            Id = 1,
            TimeBegin = DateTime.Today,
            TimeEnd = DateTime.Today,
        };

        _timeSlotRepositoryMock.Setup(repo => repo.DeleteAsync(id)!)
            .ReturnsAsync(timeSlot);
        _mapperMock.Setup(mapper => mapper.Map<GetTimeSlotDto>(timeSlot))
            .Returns(getTimeSlotDto);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object );

        // Act
        var returnedDoctorDto = await _timeSlotService.DeleteTimeSlotAsync(id);

        // Assert
        Assert.NotNull(returnedDoctorDto);
        Assert.Equal(timeSlot.TimeBegin, returnedDoctorDto.TimeBegin);
        Assert.Equal(getTimeSlotDto.Id, returnedDoctorDto.Id);
    }

    [Fact]
    public async Task GetTimeSlotsBySpecificationAsync_ReturnsIEnumerableOfTimeSlotDtos()
    {
        // Arrange
        var timeSlots = new List<TimeSlot>
        {
            new() 
            {
                Id = 1,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
                Doctors = new List<Doctor> { new() { Name = "Bob"}, new() { Name = "Pop", } },
            },
        };
        var getTimeSlotDtos = new List<GetTimeSlotDto>
        {
            new() 
            {
                Id = 1,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
        };
        var criteriaRequestDto = new AvailableTimeSlotsByCriteriaRequestDto
        {
            DoctorName = "Bob",
            Date = DateTime.Today,
            Specialty = null!,
        };

        var predicate = PredicateBuilder.New<TimeSlot>(false);
        
        predicate = predicate.And(new DoctorNameSpecification(criteriaRequestDto.DoctorName).Criteria);
        predicate = predicate.And(new TimeSlotDateSpecification(criteriaRequestDto.Date.Value).Criteria);
        
        _timeSlotRepositoryMock.Setup(repo => repo.GetAvailableTimeSlotsByPredicateAsync(
                It.Is<ExpressionStarter<TimeSlot>>(p => p.Body.Type.Equals(predicate.Body.Type))))
            .ReturnsAsync(timeSlots);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetTimeSlotDto>>(timeSlots))
            .Returns(getTimeSlotDtos);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object);
        
        // Act
        var returnedTimeSlotsDto = (await _timeSlotService.GetTimeSlotsBySpecificationsAsync(criteriaRequestDto)).ToList();
        
        // Assert
        Assert.Single(returnedTimeSlotsDto);
        Assert.IsAssignableFrom<IEnumerable<GetTimeSlotDto>>(returnedTimeSlotsDto);
        Assert.Equal(1, returnedTimeSlotsDto[0].Id);
    }

    [Fact]
    public async Task GetTimeSlotsBySpecificationAsync_UsingAllCriterias_ReturnsIEnumerableOfTimeSlotDtos()
    {
        // Arrange
        var timeSlots = new List<TimeSlot>
        {
            new() 
            {
                Id = 1,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
                Doctors = new List<Doctor> { new() { Name = "Bob", Position = "test specialty" }, new() { Name = "Pop", } },
            },
        };
        var getTimeSlotDtos = new List<GetTimeSlotDto>
        {
            new() 
            {
                Id = 1,
                TimeBegin = DateTime.Today,
                TimeEnd = DateTime.Today,
            },
        };
        var criteriaRequestDto = new AvailableTimeSlotsByCriteriaRequestDto
        {
            DoctorName = "Bob",
            Date = DateTime.Today,
            Specialty = "test specialty",
        };

        var predicate = PredicateBuilder.New<TimeSlot>(false);
        
        predicate = predicate.And(new DoctorNameSpecification(criteriaRequestDto.DoctorName).Criteria);
        predicate = predicate.And(new TimeSlotDateSpecification(criteriaRequestDto.Date.Value).Criteria);
        
        _timeSlotRepositoryMock.Setup(repo => repo.GetAvailableTimeSlotsByPredicateAsync(
                It.Is<ExpressionStarter<TimeSlot>>(p => p.Body.Type.Equals(predicate.Body.Type))))
            .ReturnsAsync(timeSlots);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetTimeSlotDto>>(timeSlots))
            .Returns(getTimeSlotDtos);

        _timeSlotService = new TimeSlotService(_patientServiceMock.Object, _timeSlotRepositoryMock.Object,
            _doctorServiceMock.Object, _mapperMock.Object);
        
        // Act
        var returnedTimeSlotsDto = (await _timeSlotService.GetTimeSlotsBySpecificationsAsync(criteriaRequestDto)).ToList();
        
        // Assert
        Assert.Single(returnedTimeSlotsDto);
        Assert.IsAssignableFrom<IEnumerable<GetTimeSlotDto>>(returnedTimeSlotsDto);
        Assert.Equal(1, returnedTimeSlotsDto[0].Id);
    }
}