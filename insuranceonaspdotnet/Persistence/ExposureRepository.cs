
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class ExposureRepository : IExposureRepository
{
    private readonly ApplicationDbContext _db;

    public ExposureRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Exposure?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Exposures
            .Include(x => x.Claim)
            .Include(x => x.PolicyCoverage)
            .Include(x => x.InsuredObject)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Exposure>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Exposures
            .AsNoTracking()
            .Include(x => x.Claim)
            .Include(x => x.PolicyCoverage)
            .Include(x => x.InsuredObject)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Exposure exposure, CancellationToken cancellationToken)
    {
        _db.Exposures.Add(exposure);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Exposure exposure, CancellationToken cancellationToken)
    {
        _db.Exposures.Update(exposure);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Exposure exposure, CancellationToken cancellationToken)
    {
        _db.Exposures.Remove(exposure);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToPaymentsAsync(
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

    public async Task RemoveFromPaymentsAsync(
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

}
