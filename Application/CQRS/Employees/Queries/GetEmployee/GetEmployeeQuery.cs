using MediatR;

namespace Application.CQRS.Employees.Queries.GetEmployee;

public class GetEmployeeQuery : IRequest<EmployeeVm>
{
    public int Id { get; set; }
}