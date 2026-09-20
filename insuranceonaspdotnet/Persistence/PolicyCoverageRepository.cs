using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class PolicyCoverageRepository : IPolicyCoverageRepository
{
    private readonly ApplicationDbContext _db;

    public PolicyCoverageRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PolicyCoverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.PolicyCoverages
            .Include(x => x.Policy)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<PolicyCoverage>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.PolicyCoverages
            .AsNoTracking()
            .Include(x => x.Policy)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(PolicyCoverage policyCoverage, CancellationToken cancellationToken)
    {
        _db.PolicyCoverages.Add(policyCoverage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(PolicyCoverage policyCoverage, CancellationToken cancellationToken)
    {
        _db.PolicyCoverages.Update(policyCoverage);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PolicyCoverage policyCoverage, CancellationToken cancellationToken)
    {
        _db.PolicyCoverages.Remove(policyCoverage);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
