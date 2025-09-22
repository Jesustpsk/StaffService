using MediatR;

namespace Application.CQRS.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}