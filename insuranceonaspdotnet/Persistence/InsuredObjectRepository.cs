using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class InsuredObjectRepository : IInsuredObjectRepository
{
    private readonly ApplicationDbContext _db;

    public InsuredObjectRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<InsuredObject?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.InsuredObjects
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<InsuredObject>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.InsuredObjects
            .AsNoTracking()
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(InsuredObject insuredObject, CancellationToken cancellationToken)
    {
        _db.InsuredObjects.Add(insuredObject);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(InsuredObject insuredObject, CancellationToken cancellationToken)
    {
        _db.InsuredObjects.Update(insuredObject);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InsuredObject insuredObject, CancellationToken cancellationToken)
    {
        _db.InsuredObjects.Remove(insuredObject);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
