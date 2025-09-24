using System.Data;
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

public class DepartmentRepositoryTests
{
    private readonly Mock<IDbConnection> _connectionMock;
    private readonly DepartmentRepository _repository;

    public DepartmentRepositoryTests()
    {
        _connectionMock = new Mock<IDbConnection>();
        var contextMock = new Mock<IStaffServiceDbContext>();
        contextMock.Setup(c => c.CreateConnectionAsync())
            .ReturnsAsync(_connectionMock.Object);

        _repository = new DepartmentRepository(contextMock.Object);
    }
    
    [Fact]
    public async Task AddAsync_ShouldReturnNewId()
    {
        // Arrange
        var department = new EntityDataGenerator<Department>().Generate();
        
        _connectionMock.SetupDapperAsync(c => c.QuerySingleAsync<int>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.AddAsync(department);

        // Assert
        result.Should().BeGreaterThan(0).And.BePositive();
    }

    [Fact]
    public async Task AddAsync_ShouldThrow_WhenDepartmentIsNull()
    {
        // Arrange
        Department? department = null;
        
        // Act
        Func<Task> act = async () => await _repository.AddAsync(department);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnAffectedRows()
    {
        // Arrange
        var department = new EntityDataGenerator<Department>().Generate();
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.UpdateAsync(department);
        
        // Assert
        result.Should().Be(1);
    }
    
    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenDepartmentNotExists()
    {
        // Arrange
        var department = new EntityDataGenerator<Department>().Generate();
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(), 
                It.IsAny<object>(), 
                null, null, null))
            .ReturnsAsync(0);
        
        // Act
        Func<Task> act = async () => await _repository.UpdateAsync(department);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{nameof(Department)}*");
    }
}