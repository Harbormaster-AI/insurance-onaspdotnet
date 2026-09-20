using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class AdjusterRepository : IAdjusterRepository
{
    private readonly ApplicationDbContext _db;

    public AdjusterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Adjuster?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Adjusters
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Adjuster>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Adjusters
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Adjuster adjuster, CancellationToken cancellationToken)
    {
        _db.Adjusters.Add(adjuster);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Adjuster adjuster, CancellationToken cancellationToken)
    {
        _db.Adjusters.Update(adjuster);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Adjuster adjuster, CancellationToken cancellationToken)
    {
        _db.Adjusters.Remove(adjuster);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
