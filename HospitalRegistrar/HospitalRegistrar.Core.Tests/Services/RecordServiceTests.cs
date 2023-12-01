using System.Collections;
using AutoMapper;
using HospitalRegistrar.Application.Exceptions;
using HospitalRegistrar.Application.Interfaces.Repositories;
using HospitalRegistrar.Application.Services;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.RecordFeatures;
using Moq;
using Record = HospitalRegistrar.Domain.Entities.Record;

namespace HospitalRegistrar.Core.Tests.Services;

public class RecordServiceTests
{
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IPatientRepository> _patientRepositoryMock = new();
    private readonly Mock<IRecordsRepository> _recordRepositoryMock = new();
    private readonly Mock<IDoctorsRepository> _doctorRepositoryMock = new();
    private readonly Mock<ITimeSlotsRepository> _timeSlotRepositoryMock = new();
    private RecordService _recordService = null!;

    [Fact]
    public async Task AddAsync_ReturnAddedRecord()
    {
        // Arrange
        var createRecordDto = new CreateRecordDto
        {
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var record = new Record
        {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var patient = new Patient { Id = createRecordDto.PatientId, Name = "Name", Age = 20 };
        var getRecordDto = new GetRecordDto {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };

        _recordRepositoryMock.Setup(repo => repo.AddAsync(record))
            .ReturnsAsync(record);
        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(record.PatientId)!)
            .ReturnsAsync(patient);
        _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(record.DoctorId)!)
            .ReturnsAsync(new Doctor());
        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(record.TimeSlotId)!)
            .ReturnsAsync(new TimeSlot());
        _mapperMock.Setup(mapper => mapper.Map<Record>(createRecordDto))
            .Returns(record);
        _mapperMock.Setup(mapper => mapper.Map<GetRecordDto>(record))
            .Returns(getRecordDto);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        var addedRecord = await _recordService.AddNewRecordAsync(createRecordDto);

        // Assert
        Assert.NotNull(addedRecord);
        Assert.IsType<GetRecordDto>(addedRecord);
        Assert.Equal(createRecordDto.Diagnosis, addedRecord.Diagnosis);
    }
    
    [Theory]
    [ClassData(typeof(RecordNullKeysTestData))]
    public async Task AddAsync_ThrowsException_IfWrongForeignKey(Patient patient, Doctor doctor, TimeSlot timeSlot)
    {
        // Arrange
        var createRecordDto = new CreateRecordDto
        {
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var record = new Record
        {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var getRecordDto = new GetRecordDto {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };

        _recordRepositoryMock.Setup(repo => repo.AddAsync(record))
            .ReturnsAsync(record);
        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(record.PatientId)!)
            .ReturnsAsync(patient);
        _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(record.DoctorId)!)
            .ReturnsAsync(doctor);
        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(record.TimeSlotId)!)
            .ReturnsAsync(timeSlot);
        _mapperMock.Setup(mapper => mapper.Map<Record>(createRecordDto))
            .Returns(record);
        _mapperMock.Setup(mapper => mapper.Map<GetRecordDto>(record))
            .Returns(getRecordDto);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>  await _recordService.AddNewRecordAsync(createRecordDto));
    }
    
    [Fact]
    public async Task GetAllAsync_ReturnIEnumerableOfRecords()
    {
        // Arrange
        var records = new List<Record>
        {
            new() {
                Id = 1,
                Diagnosis = "diag",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 1
            },
            new() {
                Id = 2,
                Diagnosis = "diag2",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 2
            },
        };
        var recordsDtos = new List<GetRecordDto>
        {
            new() {
                Id = 1,
                Diagnosis = "diag",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 1
            },
            new() {
                Id = 2,
                Diagnosis = "diag2",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 2
            },
        };

        _recordRepositoryMock.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(records);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetRecordDto>>(records))
            .Returns(recordsDtos);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        var returnedPatientsDtos = (await _recordService.GetAllRecordsAsync()).ToList();

        // Assert
        Assert.NotNull(returnedPatientsDtos);
        Assert.IsAssignableFrom<IEnumerable<GetRecordDto>>(returnedPatientsDtos);
        Assert.Equal(records.Count, returnedPatientsDtos.Count);
        Assert.Equal(records[0].Id, returnedPatientsDtos[0].Id);
        Assert.Equal(records[1].Diagnosis, returnedPatientsDtos[1].Diagnosis);
    }
    
    [Fact]
    public async Task GetPatientCardAsync_ReturnIEnumerableOfRecords()
    {
        // Arrange
        const int patientId = 2;
        var records = new List<Record>
        {
            new() {
                Id = 1,
                Diagnosis = "diag",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 1
            },
            new() {
                Id = 2,
                Diagnosis = "diag2",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 2
            },
            new() {
                Id = 2,
                Diagnosis = "diag3",
                DoctorId = 1,
                PatientId = 3,
                TimeSlotId = 2
            },
        };
        var recordsDtos = new List<GetRecordDto>
        {
            new() {
                Id = 1,
                Diagnosis = "diag",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 1
            },
            new() {
                Id = 2,
                Diagnosis = "diag2",
                DoctorId = 1,
                PatientId = 2,
                TimeSlotId = 2
            },
            new() {
                Id = 2,
                Diagnosis = "diag3",
                DoctorId = 1,
                PatientId = 3,
                TimeSlotId = 2
            },
        };
        var patient = new Patient { Id = patientId, Records = records.Where(r => r.PatientId == patientId).ToList() };

        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId)!)
            .ReturnsAsync(patient);
        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GetRecordDto>>(patient.Records))
            .Returns(recordsDtos.Where(r => r.PatientId == patientId).ToList());

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        var returnedPatientsCardDtos = (await _recordService.GetPatientCardAsync(patientId)).ToList();

        // Assert
        Assert.NotNull(returnedPatientsCardDtos);
        Assert.IsAssignableFrom<IEnumerable<GetRecordDto>>(returnedPatientsCardDtos);
        Assert.Equal(2, returnedPatientsCardDtos.Count);
        Assert.Equal(records[0].Id, returnedPatientsCardDtos[0].Id);
        Assert.Equal(records[1].Diagnosis, returnedPatientsCardDtos[1].Diagnosis);
    }
    
    [Fact]
    public async Task GetPatientCardAsync_ThrowsException_IfEntityDoesntExist()
    {
        // Arrange
        const int patientId = 2;

        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId)!)
            .ReturnsAsync((Patient)null!);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _recordService.GetPatientCardAsync(patientId));
    }
    
    [Fact]
    public async Task GetPatientCardAsync_ThrowsException_IfIdIsNegative()
    {
        // Arrange
        const int patientId = -2;

        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(patientId)!)
            .ReturnsAsync((Patient)null!);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _recordService.GetPatientCardAsync(patientId));
    }
    
    [Fact]
    public async Task GetByIdAsync_ReturnRecord()
    {
        // Arrange
        const int id = 1;
        var record = new Record 
        {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var getRecordDto = new GetRecordDto
        {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };

        _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(record);
        _mapperMock.Setup(mapper => mapper.Map<GetRecordDto>(record))
            .Returns(getRecordDto);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        var returnedPatientDto = await _recordService.GetRecordByIdAsync(id);

        // Assert
        Assert.NotNull(returnedPatientDto);
        Assert.IsType<GetRecordDto>(returnedPatientDto);
        Assert.Equal(record.Diagnosis, returnedPatientDto.Diagnosis);
        Assert.Equal(record.Id, returnedPatientDto.Id);
    }
    
    [Fact]
    public async Task GetByIdAsync_ThrowsException_IfIdIsNegative()
    {
        // Arrange
        const int id = -1;

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _recordService.GetRecordByIdAsync(id));
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnUpdatedRecord()
    {
        // Arrange
        const int id = 1;
        var updateRecordDto = new UpdateRecordDto 
        {
            Id = 1,
            Diagnosis = "diag2",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var record = new Record
        {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var patientDto = new GetRecordDto
        {
            Id = 1,
            Diagnosis = "diag2",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };

        _recordRepositoryMock.Setup(repo => repo.UpdateAsync(id, record))
            .ReturnsAsync(record);
        _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync(record);
        _mapperMock.Setup(mapper => mapper.Map<Record>(updateRecordDto))
            .Returns(record);
        _mapperMock.Setup(mapper => mapper.Map<GetRecordDto>(record))
            .Returns(patientDto);
        _patientRepositoryMock.Setup(repo => repo.GetByIdAsync(record.PatientId)!)
            .ReturnsAsync(new Patient());
        _doctorRepositoryMock.Setup(repo => repo.GetByIdAsync(record.DoctorId)!)
            .ReturnsAsync(new Doctor());
        _timeSlotRepositoryMock.Setup(repo => repo.GetByIdAsync(record.TimeSlotId)!)
            .ReturnsAsync(new TimeSlot());

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        var returnedPatientDto = await _recordService.UpdateRecordAsync(updateRecordDto);

        // Assert
        Assert.NotNull(returnedPatientDto);
        Assert.Equal(updateRecordDto.Diagnosis, returnedPatientDto.Diagnosis);
        Assert.Equal(updateRecordDto.Id, returnedPatientDto.Id);
    }
    
    [Fact]
    public async Task UpdateAsync_ThrowsException_IfEntityDoesntExist()
    {
        // Arrange
        const int id = 1;
        var updateRecordDto = new UpdateRecordDto 
        {
            Id = 1,
            Diagnosis = "diag2",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var record = new Record
        {
            Id = 1,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var patientDto = new GetRecordDto
        {
            Id = 1,
            Diagnosis = "diag2",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };

        _recordRepositoryMock.Setup(repo => repo.GetByIdAsync(id)!)
            .ReturnsAsync((Record)null!);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _recordService.UpdateRecordAsync(updateRecordDto));
    }
    
    [Fact]
    public async Task DeleteAsync_ReturnUpdatedRecord()
    {
        // Arrange
        const int id = 1;
        var record = new Record
        {
            Id = id,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };
        var getRecordDto = new GetRecordDto
        {
            Id = id,
            Diagnosis = "diag",
            DoctorId = 1,
            PatientId = 2,
            TimeSlotId = 1
        };

        _recordRepositoryMock.Setup(repo => repo.DeleteAsync(id)!)
            .ReturnsAsync(record);
        _mapperMock.Setup(mapper => mapper.Map<GetRecordDto>(record))
            .Returns(getRecordDto);

        _recordService = new RecordService(_recordRepositoryMock.Object, _patientRepositoryMock.Object,
            _doctorRepositoryMock.Object, _timeSlotRepositoryMock.Object , _mapperMock.Object );

        // Act
        var returnedDoctorDto = await _recordService.DeleteRecordAsync(id);

        // Assert
        Assert.NotNull(returnedDoctorDto);
        Assert.Equal(record.Diagnosis, returnedDoctorDto.Diagnosis);
        Assert.Equal(getRecordDto.Id, returnedDoctorDto.Id);
    }
}

public class RecordNullKeysTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { null!, null!, null! }; 
        yield return new object[] { new Patient(), null!, null! }; 
        yield return new object[] { new Patient(), new Doctor(), null! }; 
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}