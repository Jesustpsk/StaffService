using StaffService.Application.CQRS.Employees.Commands.AddEmployeeWithDependencies;
using StaffService.Application.CQRS.Employees.Commands.DeleteEmployee;
using StaffService.Application.CQRS.Employees.Commands.UpdateEmployee;
using StaffService.Application.CQRS.Employees.Queries.GetEmployee;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;
using AutoMapper;
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
    private readonly IMapper _mapper;
    
    public EmployeeController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var query = new GetEmployeesQuery();
        
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
    public async Task<IActionResult> UpdateEmployeeAsync([FromBody] UpdateEmployeeCommand command)
    {
        await _mediator.Send(command);
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        var command = new DeleteEmployeeCommand
        {
            Id = id
        };
        
        await _mediator.Send(command);

        return NoContent();
    }
}