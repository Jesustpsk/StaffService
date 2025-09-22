using Application.Common.Mapping;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Passports.Commands.AddPassport;

public class AddPassportCommand : IRequest<Unit>, IMapWith<Passport>
{
    public string Type { get; set; }
    public string Number { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Passport, AddPassportCommand>()
            .ForMember(p => p.Type,
                opt => opt.MapFrom(src => src.Type))
            .ForMember(p => p.Number,
                opt => opt.MapFrom(src => src.Number));
    }
}