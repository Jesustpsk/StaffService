using AutoMapper;
using MediatR;
using StaffService.Application.Interfaces;

namespace StaffService.Application.CQRS.Employees.Commands.UpdateEmployeeWithDependencies;

public class UpdateEmployeeWithDependenciesCommandHandler : IRequestHandler<UpdateEmployeeWithDependenciesCommand, Unit>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPassportRepository _passportRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public UpdateEmployeeWithDependenciesCommandHandler(IEmployeeRepository employeeRepository, IPassportRepository passportRepository, IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _passportRepository = passportRepository;
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEmployeeWithDependenciesCommand request, CancellationToken cancellationToken)
    {
        //TODO: тут нужно привязаться к 3 репозиториям и последовательно вызвать апдейты
        throw new Exception();
    }
}