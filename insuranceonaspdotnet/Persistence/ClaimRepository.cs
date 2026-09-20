using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ClaimRepository : IClaimRepository
{
    private readonly ApplicationDbContext _db;

    public ClaimRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Claims
            .Include(x => x.Policy)
            .Include(x => x.Customer)
            .Include(x => x.Adjuster)
            .Include(x => x.Incident)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Claim>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Claims
            .AsNoTracking()
            .Include(x => x.Policy)
            .Include(x => x.Customer)
            .Include(x => x.Adjuster)
            .Include(x => x.Incident)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Add(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Update(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Remove(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
