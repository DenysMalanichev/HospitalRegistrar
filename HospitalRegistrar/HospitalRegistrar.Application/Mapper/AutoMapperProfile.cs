using AutoMapper;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.DoctorFeatures;
using HospitalRegistrar.Features.PatientFeatures;
using HospitalRegistrar.Features.RecordFeatures;
using HospitalRegistrar.Features.TimeSlotFeatures;

namespace HospitalRegistrar.Application.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateDoctorDto, Doctor>().ReverseMap();
        CreateMap<GetDoctorDto, Doctor>().ReverseMap();
        CreateMap<UpdateDoctorDto, Doctor>().ReverseMap();
        
        CreateMap<CreatePatientDto, Patient>().ReverseMap();
        CreateMap<GetPatientDto, Patient>().ReverseMap();
        CreateMap<UpdatePatientDto, Patient>().ReverseMap();
        
        CreateMap<CreateRecordDto, Record>().ReverseMap();
        CreateMap<GetRecordDto, Record>().ReverseMap();
        CreateMap<UpdateRecordDto, Record>().ReverseMap();
        
        CreateMap<CreateTimeSlotDto, TimeSlot>().ReverseMap();
        CreateMap<GetTimeSlotDto, TimeSlot>().ReverseMap();
        CreateMap<UpdateTimeSlotDto, TimeSlot>().ReverseMap();
    }
}