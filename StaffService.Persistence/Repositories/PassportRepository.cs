using StaffService.Application.Interfaces;
using StaffService.Domain.Models;
using StaffService.Persistence.Constants;

namespace StaffService.Persistence.Repositories;

public class PassportRepository : BaseRepository<Passport>, IPassportRepository
{
    public PassportRepository(IStaffServiceDbContext context) : base(context) { }
    
    public async Task<int> AddAsync(Passport passport)
        => await QuerySingleAsync<int>(SqlCommandsConstants.AddPassport, passport);

    public async Task<int> UpdateAsync(Passport passport)
        => await ExecuteAsync(SqlCommandsConstants.UpdatePassport, passport);
}