using AutoMapper;
using StaffService.Domain.Models;
using MediatR;
using StaffService.Application.Interfaces;

namespace StaffService.Application.CQRS.Employees.Commands.AddEmployeeWithDependencies;

public class AddEmployeeWithDependenciesCommandHandler : IRequestHandler<AddEmployeeWithDependenciesCommand, int>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPassportRepository _passportRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public AddEmployeeWithDependenciesCommandHandler(IEmployeeRepository employeeRepository, IPassportRepository passportRepository, IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _passportRepository = passportRepository;
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddEmployeeWithDependenciesCommand request, CancellationToken cancellationToken)
    {
        var passport = _mapper.Map<Passport>(request);
        var passportId = await _passportRepository.AddAsync(passport);

        var department = _mapper.Map<Department>(request);
        var departmentId = await _departmentRepository.AddAsync(department);
        
        var employee = _mapper.Map<Employee>(request);
        employee.PassportId = passportId;
        employee.DepartmentId = departmentId;

        var employeeId = await _employeeRepository.AddAsync(employee);

        return employeeId;
    }
}