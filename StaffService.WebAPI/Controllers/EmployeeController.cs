using StaffService.Application.CQRS.Employees.Commands.AddEmployeeWithDependencies;
using StaffService.Application.CQRS.Employees.Commands.UpdateEmployeeWithDependencies;
using StaffService.Application.CQRS.Employees.Commands.DeleteEmployee;
using StaffService.Application.CQRS.Employees.Queries.GetEmployee;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StaffService.WebAPI.Controllers;

/// <summary>
/// 
/// </summary>
[Produces("application/json")]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEmployeeAsync(int id)
    {
        var query = new GetEmployeeQuery
        {
            Id = id
        };
        
        var employeeVm = await _mediator.Send(query);
        
        return Ok(employeeVm);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetEmployeesAsync(GetEmployeesQuery query)
    {
        var employeesVm = await _mediator.Send(query);
        
        return Ok(employeesVm);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync([FromBody] AddEmployeeWithDependenciesCommand command)
    {
        var addEmployeeCommandResult = await _mediator.Send(command);
        
        return Ok(addEmployeeCommandResult);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateEmployeeAsync([FromBody] UpdateEmployeeWithDependenciesCommand command)
    {
        await _mediator.Send(command);
        
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        var command = new DeleteEmployeeCommand
        {
            Id = id
        };
        
        await _mediator.Send(command);

        return Ok();
    }
}