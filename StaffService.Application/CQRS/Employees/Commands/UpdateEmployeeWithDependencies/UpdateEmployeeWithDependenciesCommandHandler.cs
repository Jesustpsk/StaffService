using AutoMapper;
using MediatR;
using StaffService.Application.Interfaces;
using StaffService.Domain.Models;

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
        var department = _mapper.Map<Department>(request);
        var passport = _mapper.Map<Passport>(request);
        var employee = _mapper.Map<Employee>(request);
                
        await _employeeRepository.UpdateAsync(employee);
        await _passportRepository.UpdateAsync(passport);
        await _departmentRepository.UpdateAsync(department);
        
        return Unit.Value;
    }
}