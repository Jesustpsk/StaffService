using MediatR;

namespace StaffService.Application.CQRS.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}