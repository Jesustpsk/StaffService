using Application.Common.Mapping;
using AutoMapper;
using MediatR;
using Domain.Models;

namespace Application.CQRS.Employees.Commands.AddEmployee;

public class AddEmployeeCommand : IRequest<int>, IMapWith<Employee>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public int PassportId { get; set; }
    public int DepartmentId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, AddEmployeeCommand>()
            .ForMember(evm => evm.Name,
                opt => opt.MapFrom(e => e.Name))
            .ForMember(evm => evm.Surname,
                opt => opt.MapFrom(e => e.Surname))
            .ForMember(evm => evm.Phone,
                opt => opt.MapFrom(e => e.Phone))
            .ForMember(evm => evm.CompanyId,
                opt => opt.MapFrom(e => e.CompanyId))
            .ForMember(evm => evm.PassportId,
                opt => opt.MapFrom(e => e.PassportId))
            .ForMember(evm => evm.DepartmentId,
                opt => opt.MapFrom(e => e.DepartmentId));
    }
}