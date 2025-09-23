using AutoMapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Common.Mapping;

namespace StaffService.Application.CQRS.Departments.Commands.AddDepartment;

public class AddDepartmentCommand : IRequest<int>, IMapWith<Department>
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddDepartmentCommand, Department>();
    }
}