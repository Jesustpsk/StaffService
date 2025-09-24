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

public class PassportRepositoryTests
{
    private readonly Mock<IDbConnection> _connectionMock;
    private readonly PassportRepository _repository;

    public PassportRepositoryTests()
    {
        _connectionMock = new Mock<IDbConnection>();
        var contextMock = new Mock<IStaffServiceDbContext>();
        contextMock.Setup(c => c.CreateConnectionAsync())
            .ReturnsAsync(_connectionMock.Object);

        _repository = new PassportRepository(contextMock.Object);
    }
    
    [Fact]
    public async Task AddAsync_ShouldReturnNewId()
    {
        // Arrange
        var passport = new EntityDataGenerator<Passport>().Generate();
        
        _connectionMock.SetupDapperAsync(c => c.QuerySingleAsync<int>(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.AddAsync(passport);

        // Assert
        result.Should().BeGreaterThan(0).And.BePositive();
    }

    [Fact]
    public async Task AddAsync_ShouldThrow_WhenPassportIsNull()
    {
        // Arrange
        Passport? passport = null;
        
        // Act
        Func<Task> act = async () => await _repository.AddAsync(passport);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnAffectedRows()
    {
        // Arrange
        var passport = new EntityDataGenerator<Passport>().Generate();
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(),
                It.IsAny<object>(),
                null, null, null))
            .ReturnsAsync(1);
        
        // Act
        var result = await _repository.UpdateAsync(passport);
        
        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenPassportNotExists()
    {
        // Arrange
        var passport = new EntityDataGenerator<Passport>().Generate();
        
        _connectionMock.SetupDapperAsync(c => c.ExecuteAsync(
                It.IsAny<string>(), 
                It.IsAny<object>(), 
                null, null, null))
            .ReturnsAsync(0);
        
        // Act
        Func<Task> act = async () => await _repository.UpdateAsync(passport);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"*{nameof(Passport)}*");
    }
}