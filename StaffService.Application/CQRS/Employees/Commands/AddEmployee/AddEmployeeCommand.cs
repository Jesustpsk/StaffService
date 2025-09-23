using AutoMapper;
using MediatR;
using StaffService.Domain.Models;
using StaffService.Application.Common.Mapping;

namespace StaffService.Application.CQRS.Employees.Commands.AddEmployee;

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
        profile.CreateMap<AddEmployeeCommand, Employee>();
    }
}