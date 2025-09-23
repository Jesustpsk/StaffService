using AutoMapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Interfaces;

namespace StaffService.Application.CQRS.Passports.Commands.AddPassport;

public class AddPassportCommandHandler : IRequestHandler<AddPassportCommand, int>
{
    private readonly IPassportRepository _passportRepository;
    private readonly IMapper _mapper;

    public AddPassportCommandHandler(IPassportRepository passportRepository, IMapper mapper)
    {
        _passportRepository = passportRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddPassportCommand request, CancellationToken cancellationToken)
    {
        var passport = _mapper.Map<Passport>(request);
        
        var result = await _passportRepository.AddAsync(passport);
        
        return result;
    }
}