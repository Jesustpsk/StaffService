using AutoMapper;
using StaffService.Domain.Models;
using StaffService.Application.Common.Mapping;

namespace StaffService.Application.CQRS.Employees.Queries;

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
        profile.CreateMap<Employee, EmployeeVm>();
    }
}