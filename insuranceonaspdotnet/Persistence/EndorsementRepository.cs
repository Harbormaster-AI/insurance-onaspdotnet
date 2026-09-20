using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class EndorsementRepository : IEndorsementRepository
{
    private readonly ApplicationDbContext _db;

    public EndorsementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Endorsement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Endorsements
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Endorsement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Endorsements
            .AsNoTracking()
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Endorsement endorsement, CancellationToken cancellationToken)
    {
        _db.Endorsements.Add(endorsement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Endorsement endorsement, CancellationToken cancellationToken)
    {
        _db.Endorsements.Update(endorsement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Endorsement endorsement, CancellationToken cancellationToken)
    {
        _db.Endorsements.Remove(endorsement);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
