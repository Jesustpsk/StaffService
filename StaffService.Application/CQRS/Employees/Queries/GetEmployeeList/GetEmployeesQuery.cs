using MediatR;

namespace StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;

public class GetEmployeesQuery : IRequest<List<EmployeeVm>>
{
    public int? CompanyId { get; set; }
    public string? DepartmentName { get; set; }
}