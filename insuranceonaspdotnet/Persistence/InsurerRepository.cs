using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class InsurerRepository : IInsurerRepository
{
    private readonly ApplicationDbContext _db;

    public InsurerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Insurer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Insurers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Insurer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Insurers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Insurer insurer, CancellationToken cancellationToken)
    {
        _db.Insurers.Add(insurer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Insurer insurer, CancellationToken cancellationToken)
    {
        _db.Insurers.Update(insurer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Insurer insurer, CancellationToken cancellationToken)
    {
        _db.Insurers.Remove(insurer);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
