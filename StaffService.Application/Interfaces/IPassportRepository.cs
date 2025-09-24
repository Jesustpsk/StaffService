using StaffService.Domain.Models;

namespace StaffService.Application.Interfaces;

public interface IPassportRepository
{
    public Task<int> AddAsync(Passport passport);
    public Task<int> UpdateAsync(Passport employee);
}