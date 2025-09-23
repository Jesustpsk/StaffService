using MediatR;
using StaffService.Application.Common.Exceptions;
using StaffService.Application.Interfaces;
using StaffService.Domain.Models;

namespace StaffService.Application.CQRS.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var affectedRows = await _employeeRepository.UpdateAsync(request);
        
        if(affectedRows == 0)
            throw new NotFoundException(nameof(Employee), request.Id);
            
        return Unit.Value;
    }
}