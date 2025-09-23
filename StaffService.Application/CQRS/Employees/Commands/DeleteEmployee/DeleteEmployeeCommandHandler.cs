using MediatR;
using StaffService.Application.Common.Exceptions;
using StaffService.Application.Interfaces;
using StaffService.Domain.Models;

namespace StaffService.Application.CQRS.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Unit>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var affectedRows = await _employeeRepository.DeleteAsync(request.Id);
        
        if(affectedRows == 0)
            throw new NotFoundException(nameof(Employee), request.Id);
        
        return Unit.Value;
    }
}