using AutoMapper;
using Domain.Dtos;
using Domain.Entites;

namespace Infrastructure.InfrastructuraMapper;

public class ServicesProfile : Profile
{
    public ServicesProfile()
    {
        CreateMap<Employee, AddEmployeeDto>().ReverseMap();
        CreateMap<Attendance, AddAttendanceDto>().ReverseMap();
        CreateMap<Attendance, GetAttendanceDto>();
        CreateMap<Employee, GetEmployeeDto>()
        .ForMember(e=>e.Attendances,conf=>conf.MapFrom(x=>x.Attendances));
    }
}
