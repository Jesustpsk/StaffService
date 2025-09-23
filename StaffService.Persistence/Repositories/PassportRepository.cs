using StaffService.Application.Interfaces;
using Dapper;
using StaffService.Domain.Models;
using StaffService.Persistence.Constants;

namespace StaffService.Persistence.Repositories;

public class PassportRepository : BaseRepository<Passport>, IPassportRepository
{
    public PassportRepository(IStaffServiceDbContext context) : base(context) { }
    
    public async Task<int> AddAsync(Passport passport)
    {
        var query = SqlCommandsConstants.AddPassport;

        var passportId = await Connection.QuerySingleAsync<int>(query, passport);
        return passportId;
    }
}