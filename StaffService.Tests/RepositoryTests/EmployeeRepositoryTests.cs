using System.Data;
using Bogus;
using Dapper;
using FluentAssertions;
using Moq;
using Moq.Dapper;
using StaffService.Application.Common.Exceptions;
using StaffService.Application.Interfaces;
using StaffService.Domain.Models;
using StaffService.Persistence.Repositories;
using StaffService.Tests.Bogus;
using Xunit;

namespace StaffService.Tests.RepositoryTests;

public class EmployeeRepositoryTests
{
    private readonly Mock<IDbConnection> _connectionMock;
    private readonly EmployeeRepository _repository;

    public EmployeeRepositoryTests()
    {
        _connectionMock = new Mock<IDbConnection>();
        var contextMock = new Mock<IStaffServiceDbContext>();
        contextMock.Setup(c => c.CreateConnectionAsync())
            .ReturnsAsync(_connectionMock.Object);

        _repository = new EmployeeRepository(contextMock.Object);
    }
    
    [Fact]
    public async Task AddAsync_ShouldReturnNewId()
    {
        // Arrange
        var passport = new EntityDataGenerator<Passport>().Generate();
        var department = new EntityDataGenerator<Department>().Generate();

        var employee = new EntityDataGenerator<Employee>()
            .Generate();
        employee.Passport = passport;
        employee.Department = department;
        
        _connectionMock.SetupDapperAsync(c => c.QuerySingleAsync<int>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.AddAsync(employee);

        // Assert
        result.Should().BeGreaterThan(0).And.BePositive();
    }

    [Fact]
    public async Task AddAsync_ShouldThrow_WhenEmployeeIsNull()
    {
        // Arrange
        Employee? employee = null;
        
        // Act
        Func<Task> act = async () => await _repository.AddAsync(employee);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnAffectedRows()
    {
        // Arrange
        var passport = new EntityDataGenerator<Passport>().Generate();
        var department = new EntityDataGenerator<Department>().Generate();

        var employee = new EntityDataGenerator<Employee>()
            .Generate();
        employee.Passport = passport;
        employee.Department = department;
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.UpdateAsync(employee);
        
        // Assert
        result.Should().Be(1);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenEmployeeNotExists()
    {
        // Arrange
        var passport = new EntityDataGenerator<Passport>().Generate();
        var department = new EntityDataGenerator<Department>().Generate();

        var employee = new EntityDataGenerator<Employee>()
            .Generate();
        employee.Passport = passport;
        employee.Department = department;
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(), 
                It.IsAny<object>(), 
                null, null, null))
            .ReturnsAsync(0);
        
        // Act
        Func<Task> act = async () => await _repository.UpdateAsync(employee);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{nameof(Employee)}*");
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnAffectedRows()
    {
        // Arrange
        var fakeId = new Faker().Random.Int();
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.DeleteAsync(fakeId);
        
        // Assert
        result.Should().Be(1);
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenEmployeeNotExists()
    {
        // Arrange
        var fakeId = new Faker().Random.Int();
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(), 
                It.IsAny<object>(), 
                null, null, null))
            .ReturnsAsync(0);
        
        // Act
        Func<Task> act = async () => await _repository.DeleteAsync(fakeId);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{nameof(Employee)}*");
    }
}