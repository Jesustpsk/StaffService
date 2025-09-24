using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StaffService.Application.Common.Exceptions;
using StaffService.Application.CQRS.Employees.Commands.AddEmployeeWithDependencies;
using StaffService.Application.CQRS.Employees.Commands.DeleteEmployee;
using StaffService.Application.CQRS.Employees.Commands.UpdateEmployeeWithDependencies;
using StaffService.Application.CQRS.Employees.Queries;
using StaffService.Application.CQRS.Employees.Queries.GetEmployee;
using StaffService.Application.CQRS.Employees.Queries.GetEmployeeList;
using StaffService.Domain.Models;
using StaffService.Tests.Bogus;
using StaffService.WebAPI.Controllers;
using Xunit;

namespace StaffService.Tests.ControllerTests;

public class EmployeeControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly EmployeeController _employeeController;
    
    public EmployeeControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _employeeController = new EmployeeController(_mediatorMock.Object);
    }
    
    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnOk_WithEmployee()
    {
        // Arrange
        var id = new Faker().Random.Int();
        var employeeVm = new EntityDataGenerator<EmployeeVm>().Generate();
        
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetEmployeeQuery>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(employeeVm);

        // Act
        var result = await _employeeController.GetEmployeeAsync(id);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().Be(employeeVm);
    }
    
    [Fact]
    public async Task GetEmployeeAsync_ShouldReturnOk_WithoutEmployee()
    {
        // Arrange
        var id = new Faker().Random.Int();
        EmployeeVm employeeVm = null;
        
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetEmployeeQuery>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(employeeVm);

        // Act
        var result = await _employeeController.GetEmployeeAsync(id);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().Be(employeeVm);
    }

    [Fact]
    public async Task GetEmployeesAsync_ShouldReturnOk_WithEmployeeList()
    {
        // Arrange
        var listVm = new List<EmployeeVm> { 
            new EntityDataGenerator<EmployeeVm>().Generate(), 
            new EntityDataGenerator<EmployeeVm>().Generate(), 
            new EntityDataGenerator<EmployeeVm>().Generate() 
        };
        
        _mediatorMock.Setup(m => m.Send(
            It.IsAny<GetEmployeesQuery>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(listVm);
        
        // Act
        var result = await _employeeController.GetEmployeesAsync(new GetEmployeesQuery());

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().Be(listVm);
    }
    
    [Fact]
    public async Task GetEmployeesAsync_ShouldReturnOk_WithoutEmployeeList()
    {
        // Arrange
        var listVm = new List<EmployeeVm>();
        
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<GetEmployeesQuery>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(listVm);
        
        // Act
        var result = await _employeeController.GetEmployeesAsync(new GetEmployeesQuery());

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().Be(listVm);
    }

    [Fact]
    public async Task AddEmployeeAsync_ShouldReturnOk_WithEmployeeId()
    {
        // Arrange
        var command = new EntityDataGenerator<AddEmployeeWithDependenciesCommand>().Generate();
        var commandResult = new Faker().Random.Int();
        
        _mediatorMock.Setup(m => m.Send(
            It.IsAny<AddEmployeeWithDependenciesCommand>(), 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(commandResult);
        
        // Act
        var result = await _employeeController.AddEmployeeAsync(command);
        
        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult!.Value.Should().Be(commandResult);
    }
    
    [Fact]
    public async Task AddEmployeeAsync_ShouldThrow_WhenEmployeeIsNull()
    {
        // Arrange
        var employee = new EntityDataGenerator<AddEmployeeWithDependenciesCommand>(true).Generate();
        _mediatorMock.Setup(m => m.Send(
                It.IsAny<AddEmployeeWithDependenciesCommand>(), 
                It.IsAny<CancellationToken>()))
            .Throws<ArgumentNullException>();
        
        // Act
        Func<Task> act = async () => await _employeeController.AddEmployeeAsync(employee);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ShouldReturnOk()
    {
        // Assert
        var command = new EntityDataGenerator<UpdateEmployeeWithDependenciesCommand>().Generate();

        _mediatorMock.Setup(m => m.Send(
                It.IsAny<UpdateEmployeeWithDependenciesCommand>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);
        
        // Act
        var result = await _employeeController.UpdateEmployeeAsync(command);
        
        // Assert
        result.Should().BeOfType<OkResult>();
    }
    
    [Fact]
    public async Task UpdateEmployeeAsync_ShouldThrow_WhenEmployeeIsNull()
    {
        // Assert
        var command = new EntityDataGenerator<UpdateEmployeeWithDependenciesCommand>(true).Generate();

        _mediatorMock.Setup(m => m.Send(
                It.IsAny<UpdateEmployeeWithDependenciesCommand>(),
                It.IsAny<CancellationToken>()))
            .Throws<ArgumentNullException>();
        
        // Act
        Func<Task> act = async () => await _employeeController.UpdateEmployeeAsync(command);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
    
    [Fact]
    public async Task UpdateEmployeeAsync_ShouldThrow_WhenEmployeeIsNotFound()
    {
        // Assert
        var command = new EntityDataGenerator<UpdateEmployeeWithDependenciesCommand>().Generate();

        _mediatorMock.Setup(m => m.Send(
                It.IsAny<UpdateEmployeeWithDependenciesCommand>(),
                It.IsAny<CancellationToken>()))
            .Throws<NotFoundException>();
        
        // Act
        Func<Task> act = async () => await _employeeController.UpdateEmployeeAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task DeleteEmployeeAsync_ShouldReturnOk()
    {
        // Arrange
        var id = new Faker().Random.Int();
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteEmployeeCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _employeeController.DeleteEmployeeAsync(id);

        // Assert
        result.Should().BeOfType<OkResult>();
    }
    
    [Fact]
    public async Task DeleteEmployeeAsync_ShouldThrow_WhenEmployeeIsNotFound()
    {
        // Assert
        var id = new Faker().Random.Int();

        _mediatorMock.Setup(m => m.Send(
                It.IsAny<DeleteEmployeeCommand>(),
                It.IsAny<CancellationToken>()))
            .Throws<NotFoundException>();
        
        // Act
        Func<Task> act = async () => await _employeeController.DeleteEmployeeAsync(id);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}