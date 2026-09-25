
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ClaimReserveRepository : IClaimReserveRepository
{
    private readonly ApplicationDbContext _db;

    public ClaimReserveRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ClaimReserve?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ClaimReserves
            .Include(x => x.Claim)
            .Include(x => x.Exposure)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ClaimReserve>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ClaimReserves
            .AsNoTracking()
            .Include(x => x.Claim)
            .Include(x => x.Exposure)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ClaimReserve claimReserve, CancellationToken cancellationToken)
    {
        _db.ClaimReserves.Add(claimReserve);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ClaimReserve claimReserve, CancellationToken cancellationToken)
    {
        _db.ClaimReserves.Update(claimReserve);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ClaimReserve claimReserve, CancellationToken cancellationToken)
    {
        _db.ClaimReserves.Remove(claimReserve);
        await _db.SaveChangesAsync(cancellationToken);
    }

}
