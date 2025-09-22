using Domain.Models;

namespace Application.Interfaces;

public interface IPassportRepository
{
    public Task<int> AddPassport(Passport passport);
}