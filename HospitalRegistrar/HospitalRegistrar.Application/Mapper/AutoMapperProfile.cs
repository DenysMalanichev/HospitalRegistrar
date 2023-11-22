using AutoMapper;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.DoctorFeatures;
using HospitalRegistrar.Features.PatientFeatures;

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
    }
}