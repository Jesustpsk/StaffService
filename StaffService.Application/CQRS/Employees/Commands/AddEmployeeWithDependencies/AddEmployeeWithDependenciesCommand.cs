using AutoMapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Common.Mapping;

namespace StaffService.Application.CQRS.Employees.Commands.AddEmployeeWithDependencies;

public class AddEmployeeWithDependenciesCommand : IRequest<int>, IMapWith<Employee>, IMapWith<Passport>,
    IMapWith<Department>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public string PassportType { get; set; }
    public string PassportNumber { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentPhone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddEmployeeWithDependenciesCommand, Employee>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname,
                opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Phone,
                opt => opt.MapFrom(src => src.Phone))
            .ForMember(dest => dest.CompanyId,
                opt => opt.MapFrom(src => src.CompanyId));

        profile.CreateMap<AddEmployeeWithDependenciesCommand, Passport>()
            .ForMember(dest => dest.Type,
                opt => opt.MapFrom(src => src.PassportType))
            .ForMember(dest => dest.Number,
                opt => opt.MapFrom(src => src.PassportNumber));

        profile.CreateMap<AddEmployeeWithDependenciesCommand, Department>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.Phone,
                opt => opt.MapFrom(src => src.DepartmentPhone));
    }
}