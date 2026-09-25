
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class InsurerRepository : IInsurerRepository
{
    private readonly ApplicationDbContext _db;

    public InsurerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Insurer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Insurers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Insurer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Insurers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Insurer insurer, CancellationToken cancellationToken)
    {
        _db.Insurers.Add(insurer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Insurer insurer, CancellationToken cancellationToken)
    {
        _db.Insurers.Update(insurer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Insurer insurer, CancellationToken cancellationToken)
    {
        _db.Insurers.Remove(insurer);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToProductsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsuranceProducts
            .Where(insuranceProduct =>
                request.ChildIds.Contains(insuranceProduct.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insuranceProduct =>
                        EF.Property<Guid?>(
                            insuranceProduct,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromProductsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsuranceProducts
            .Where(insuranceProduct =>
                request.ChildIds.Contains(insuranceProduct.Id) &&
                EF.Property<Guid?>(
                    insuranceProduct,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insuranceProduct =>
                        EF.Property<Guid?>(
                            insuranceProduct,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToDistributionPartnersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Distributors
            .Where(distributor =>
                request.ChildIds.Contains(distributor.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    distributor =>
                        EF.Property<Guid?>(
                            distributor,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromDistributionPartnersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Distributors
            .Where(distributor =>
                request.ChildIds.Contains(distributor.Id) &&
                EF.Property<Guid?>(
                    distributor,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    distributor =>
                        EF.Property<Guid?>(
                            distributor,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromPoliciesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Policys
            .Where(policy =>
                request.ChildIds.Contains(policy.Id) &&
                EF.Property<Guid?>(
                    policy,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policy =>
                        EF.Property<Guid?>(
                            policy,
                            "Document_Id"),
                    (Guid?)null));
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


    public async Task AddToReinsuranceAgreementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ReinsuranceAgreements
            .Where(reinsuranceAgreement =>
                request.ChildIds.Contains(reinsuranceAgreement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    reinsuranceAgreement =>
                        EF.Property<Guid?>(
                            reinsuranceAgreement,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromReinsuranceAgreementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.ReinsuranceAgreements
            .Where(reinsuranceAgreement =>
                request.ChildIds.Contains(reinsuranceAgreement.Id) &&
                EF.Property<Guid?>(
                    reinsuranceAgreement,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    reinsuranceAgreement =>
                        EF.Property<Guid?>(
                            reinsuranceAgreement,
                            "Document_Id"),
                    (Guid?)null));
    }

}
