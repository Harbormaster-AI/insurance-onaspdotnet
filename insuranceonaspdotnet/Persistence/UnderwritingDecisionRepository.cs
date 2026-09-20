using insuranceonaspdotnet.Domain;
using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class UnderwritingDecisionRepository : IUnderwritingDecisionRepository
{
    private readonly ApplicationDbContext _db;

    public UnderwritingDecisionRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<UnderwritingDecision?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.UnderwritingDecisions
            .Include(x => x.Quote)
            .Include(x => x.Underwriter)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<UnderwritingDecision>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.UnderwritingDecisions
            .AsNoTracking()
            .Include(x => x.Quote)
            .Include(x => x.Underwriter)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(UnderwritingDecision underwritingDecision, CancellationToken cancellationToken)
    {
        _db.UnderwritingDecisions.Add(underwritingDecision);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UnderwritingDecision underwritingDecision, CancellationToken cancellationToken)
    {
        _db.UnderwritingDecisions.Update(underwritingDecision);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(UnderwritingDecision underwritingDecision, CancellationToken cancellationToken)
    {
        _db.UnderwritingDecisions.Remove(underwritingDecision);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
