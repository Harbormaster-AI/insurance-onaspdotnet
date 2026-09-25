
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ClaimRepository : IClaimRepository
{
    private readonly ApplicationDbContext _db;

    public ClaimRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Claims
            .Include(x => x.Policy)
            .Include(x => x.Customer)
            .Include(x => x.Adjuster)
            .Include(x => x.Incident)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Claim>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Claims
            .AsNoTracking()
            .Include(x => x.Policy)
            .Include(x => x.Customer)
            .Include(x => x.Adjuster)
            .Include(x => x.Incident)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Add(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Update(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Claim claim, CancellationToken cancellationToken)
    {
        _db.Claims.Remove(claim);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToExposuresAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Exposures
            .Where(exposure =>
                request.ChildIds.Contains(exposure.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    exposure =>
                        EF.Property<Guid?>(
                            exposure,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromExposuresAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Exposures
            .Where(exposure =>
                request.ChildIds.Contains(exposure.Id) &&
                EF.Property<Guid?>(
                    exposure,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    exposure =>
                        EF.Property<Guid?>(
                            exposure,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToReservesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ClaimReserves
            .Where(claimReserve =>
                request.ChildIds.Contains(claimReserve.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claimReserve =>
                        EF.Property<Guid?>(
                            claimReserve,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromReservesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ClaimReserves
            .Where(claimReserve =>
                request.ChildIds.Contains(claimReserve.Id) &&
                EF.Property<Guid?>(
                    claimReserve,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claimReserve =>
                        EF.Property<Guid?>(
                            claimReserve,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToClaimPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ClaimPayments
            .Where(claimPayment =>
                request.ChildIds.Contains(claimPayment.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claimPayment =>
                        EF.Property<Guid?>(
                            claimPayment,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromClaimPaymentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ClaimPayments
            .Where(claimPayment =>
                request.ChildIds.Contains(claimPayment.Id) &&
                EF.Property<Guid?>(
                    claimPayment,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    claimPayment =>
                        EF.Property<Guid?>(
                            claimPayment,
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


    public async Task AddToSubrogationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SubrogationRecoverys
            .Where(subrogationRecovery =>
                request.ChildIds.Contains(subrogationRecovery.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    subrogationRecovery =>
                        EF.Property<Guid?>(
                            subrogationRecovery,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromSubrogationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.SubrogationRecoverys
            .Where(subrogationRecovery =>
                request.ChildIds.Contains(subrogationRecovery.Id) &&
                EF.Property<Guid?>(
                    subrogationRecovery,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    subrogationRecovery =>
                        EF.Property<Guid?>(
                            subrogationRecovery,
                            "Document_Id"),
                    (Guid?)null));
    }

}
