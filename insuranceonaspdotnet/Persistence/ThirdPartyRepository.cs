using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ThirdPartyRepository : IThirdPartyRepository
{
    private readonly ApplicationDbContext _db;

    public ThirdPartyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ThirdParty?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ThirdPartys
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ThirdParty>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ThirdPartys
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ThirdParty thirdParty, CancellationToken cancellationToken)
    {
        _db.ThirdPartys.Add(thirdParty);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ThirdParty thirdParty, CancellationToken cancellationToken)
    {
        _db.ThirdPartys.Update(thirdParty);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ThirdParty thirdParty, CancellationToken cancellationToken)
    {
        _db.ThirdPartys.Remove(thirdParty);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
