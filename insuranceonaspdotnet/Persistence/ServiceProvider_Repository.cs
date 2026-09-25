
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ServiceProvider_Repository : IServiceProvider_Repository
{
    private readonly ApplicationDbContext _db;

    public ServiceProvider_Repository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceProvider_?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.ServiceProvider_s
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<ServiceProvider_>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.ServiceProvider_s
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ServiceProvider_ serviceProvider_, CancellationToken cancellationToken)
    {
        _db.ServiceProvider_s.Add(serviceProvider_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ServiceProvider_ serviceProvider_, CancellationToken cancellationToken)
    {
        _db.ServiceProvider_s.Update(serviceProvider_);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ServiceProvider_ serviceProvider_, CancellationToken cancellationToken)
    {
        _db.ServiceProvider_s.Remove(serviceProvider_);
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

}
