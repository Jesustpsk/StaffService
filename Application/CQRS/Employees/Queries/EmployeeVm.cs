using Application.Common.Mapping;
using AutoMapper;
using Domain.Models;

namespace Application.CQRS.Employees.Queries;

public class EmployeeVm : IMapWith<Employee>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public Passport Passport { get; set; }
    public Department Department { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeVm>()
            .ForMember(evm => evm.Id,
                opt => opt.MapFrom(e => e.Id))
            .ForMember(evm => evm.Name,
                opt => opt.MapFrom(e => e.Name))
            .ForMember(evm => evm.Surname,
                opt => opt.MapFrom(e => e.Surname))
            .ForMember(evm => evm.Phone,
                opt => opt.MapFrom(e => e.Phone))
            .ForMember(evm => evm.CompanyId,
                opt => opt.MapFrom(e => e.CompanyId))
            .ForMember(evm => evm.Passport,
                opt => opt.MapFrom(e => e.Passport))
            .ForMember(evm => evm.Department,
                opt => opt.MapFrom(e => e.Department));
    }
}