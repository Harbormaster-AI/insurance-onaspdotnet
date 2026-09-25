
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class UnderwriterRepository : IUnderwriterRepository
{
    private readonly ApplicationDbContext _db;

    public UnderwriterRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Underwriter?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Underwriters
            .Include(x => x.Insurer)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Underwriter>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Underwriters
            .AsNoTracking()
            .Include(x => x.Insurer)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Underwriter underwriter, CancellationToken cancellationToken)
    {
        _db.Underwriters.Add(underwriter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Underwriter underwriter, CancellationToken cancellationToken)
    {
        _db.Underwriters.Update(underwriter);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Underwriter underwriter, CancellationToken cancellationToken)
    {
        _db.Underwriters.Remove(underwriter);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToDecisionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.UnderwritingDecisions
            .Where(underwritingDecision =>
                request.ChildIds.Contains(underwritingDecision.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    underwritingDecision =>
                        EF.Property<Guid?>(
                            underwritingDecision,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDecisionsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.UnderwritingDecisions
            .Where(underwritingDecision =>
                request.ChildIds.Contains(underwritingDecision.Id) &&
                EF.Property<Guid?>(
                    underwritingDecision,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    underwritingDecision =>
                        EF.Property<Guid?>(
                            underwritingDecision,
                            "Document_Id"),
                    (Guid?)null));
    }

}
