using AutoMapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Common.Mapping;

namespace StaffService.Application.CQRS.Passports.Commands.AddPassport;

public class AddPassportCommand : IRequest<int>, IMapWith<Passport>
{
    public string Type { get; set; }
    public string Number { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<AddPassportCommand, Passport>();
    }
}