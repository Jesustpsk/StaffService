using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Passports.Commands.AddPassport;

public class AddPassportCommandHandler : IRequestHandler<AddPassportCommand, Unit>
{
    private readonly IPassportRepository _passportRepository;
    private readonly IMapper _mapper;

    public AddPassportCommandHandler(IPassportRepository passportRepository, IMapper mapper)
    {
        _passportRepository = passportRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddPassportCommand request, CancellationToken cancellationToken)
    {
        var passport = _mapper.Map<Passport>(request);
        
        await _passportRepository.AddPassport(passport);
        
        return Unit.Value;
    }
}