using MediatR;

namespace Application.CQRS.Employees.Queries.GetEmployeeList;

public class GetEmployeesQuery : IRequest<List<EmployeeVm>>
{
    
}