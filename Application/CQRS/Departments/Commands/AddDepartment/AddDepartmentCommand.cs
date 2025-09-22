using Application.Common.Mapping;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Departments.Commands.AddDepartment;

public class AddDepartmentCommand : IRequest<Unit>, IMapWith<Department>
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, AddDepartmentCommand>()
            .ForMember(d => d.Name,
                opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Phone,
                opt => opt.MapFrom(s => s.Phone));
    }
}