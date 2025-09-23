using MediatR;

namespace StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;

public class GetEmployeesQuery : IRequest<List<EmployeeVm>>
{
}