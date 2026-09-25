
using insuranceonaspdotnet.Contracts;
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


    public async Task AddToClaimsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Claims
            .Where(claim =>
                request.ChildIds.Contains(claim.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claim =>
                        EF.Property<Guid?>(
                            claim,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromClaimsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Claims
            .Where(claim =>
                request.ChildIds.Contains(claim.Id) &&
                EF.Property<Guid?>(
                    claim,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claim =>
                        EF.Property<Guid?>(
                            claim,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToServiceProvidersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ServiceProvider_s
            .Where(serviceProvider_ =>
                request.ChildIds.Contains(serviceProvider_.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    serviceProvider_ =>
                        EF.Property<Guid?>(
                            serviceProvider_,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromServiceProvidersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ServiceProvider_s
            .Where(serviceProvider_ =>
                request.ChildIds.Contains(serviceProvider_.Id) &&
                EF.Property<Guid?>(
                    serviceProvider_,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    serviceProvider_ =>
                        EF.Property<Guid?>(
                            serviceProvider_,
                            "Document_Id"),
                    (Guid?)null));
    }

}
