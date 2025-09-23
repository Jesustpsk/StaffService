using MediatR;

namespace StaffService.Application.CQRS.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
}