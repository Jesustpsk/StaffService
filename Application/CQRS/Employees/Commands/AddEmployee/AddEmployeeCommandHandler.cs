using Application.Interfaces;
using AutoMapper;
using MediatR;
using Domain.Models;

namespace Application.CQRS.Employees.Commands.AddEmployee;

public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, int>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public AddEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _mapper.Map<Employee>(request);
        
        var employeeId = await _employeeRepository.AddEmployeeAsync(employee);
        
        return employeeId;
    }
}