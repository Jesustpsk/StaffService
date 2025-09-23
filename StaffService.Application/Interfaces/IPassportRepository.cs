using System.Data;
using StaffService.Domain.Models;

namespace StaffService.Application.Interfaces;

public interface IPassportRepository
{
    public Task<int> AddAsync(Passport passport);
}