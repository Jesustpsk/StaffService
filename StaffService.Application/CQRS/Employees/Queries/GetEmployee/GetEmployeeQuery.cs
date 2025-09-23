using MediatR;

namespace StaffService.Application.CQRS.Employees.Queries.GetEmployee;

public class GetEmployeeQuery : IRequest<EmployeeVm>
{
    public int Id { get; set; }
}