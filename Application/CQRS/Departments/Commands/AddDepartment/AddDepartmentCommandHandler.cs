using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.CQRS.Departments.Commands.AddDepartment;

public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, Unit>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public AddDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = _mapper.Map<Department>(request);
        
        await _departmentRepository.AddDepartment(department);
        
        return Unit.Value;
    }
}