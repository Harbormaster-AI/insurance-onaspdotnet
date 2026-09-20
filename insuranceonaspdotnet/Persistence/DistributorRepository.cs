using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class DistributorRepository : IDistributorRepository
{
    private readonly ApplicationDbContext _db;

    public DistributorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Distributor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Distributors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Distributor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Distributors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Distributor distributor, CancellationToken cancellationToken)
    {
        _db.Distributors.Add(distributor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Distributor distributor, CancellationToken cancellationToken)
    {
        _db.Distributors.Update(distributor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Distributor distributor, CancellationToken cancellationToken)
    {
        _db.Distributors.Remove(distributor);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
