using Application.Interfaces;
using Domain.Models;

namespace Persistence.Repositories;

public class PassportRepository : IPassportRepository
{
    public Task<int> AddPassport(Passport passport)
    {
        throw new NotImplementedException();
    }
}