using AutoMapper;
using HospitalRegistrar.Domain.Entities;
using HospitalRegistrar.Features.DoctorFeatures;

namespace HospitalRegistrar.Application.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateDoctorDto, Doctor>().ReverseMap();
        CreateMap<GetDoctorDto, Doctor>().ReverseMap();
        CreateMap<UpdateDoctorDto, Doctor>().ReverseMap();
    }
}