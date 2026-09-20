using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ReinsuranceAgreementRepository : IReinsuranceAgreementRepository
{
    private readonly ApplicationDbContext _db;

    public ReinsuranceAgreementRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ReinsuranceAgreement?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ReinsuranceAgreements
            .Include(x => x.Insurer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ReinsuranceAgreement>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ReinsuranceAgreements
            .AsNoTracking()
            .Include(x => x.Insurer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ReinsuranceAgreement reinsuranceAgreement, CancellationToken cancellationToken)
    {
        _db.ReinsuranceAgreements.Add(reinsuranceAgreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ReinsuranceAgreement reinsuranceAgreement, CancellationToken cancellationToken)
    {
        _db.ReinsuranceAgreements.Update(reinsuranceAgreement);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReinsuranceAgreement reinsuranceAgreement, CancellationToken cancellationToken)
    {
        _db.ReinsuranceAgreements.Remove(reinsuranceAgreement);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
