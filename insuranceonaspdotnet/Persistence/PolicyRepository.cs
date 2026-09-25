
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class PolicyRepository : IPolicyRepository
{
    private readonly ApplicationDbContext _db;

    public PolicyRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Policys
            .Include(x => x.Insurer)
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Include(x => x.Agent)
            .Include(x => x.BillingAccount)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Policys
            .AsNoTracking()
            .Include(x => x.Insurer)
            .Include(x => x.Customer)
            .Include(x => x.Product)
            .Include(x => x.Agent)
            .Include(x => x.BillingAccount)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Add(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Update(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Policy policy, CancellationToken cancellationToken)
    {
        _db.Policys.Remove(policy);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToCoveragesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PolicyCoverages
            .Where(policyCoverage =>
                request.ChildIds.Contains(policyCoverage.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policyCoverage =>
                        EF.Property<Guid?>(
                            policyCoverage,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCoveragesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.PolicyCoverages
            .Where(policyCoverage =>
                request.ChildIds.Contains(policyCoverage.Id) &&
                EF.Property<Guid?>(
                    policyCoverage,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    policyCoverage =>
                        EF.Property<Guid?>(
                            policyCoverage,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToInsuredObjectsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsuredObjects
            .Where(insuredObject =>
                request.ChildIds.Contains(insuredObject.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insuredObject =>
                        EF.Property<Guid?>(
                            insuredObject,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInsuredObjectsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.InsuredObjects
            .Where(insuredObject =>
                request.ChildIds.Contains(insuredObject.Id) &&
                EF.Property<Guid?>(
                    insuredObject,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insuredObject =>
                        EF.Property<Guid?>(
                            insuredObject,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToEndorsementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Endorsements
            .Where(endorsement =>
                request.ChildIds.Contains(endorsement.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    endorsement =>
                        EF.Property<Guid?>(
                            endorsement,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromEndorsementsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Endorsements
            .Where(endorsement =>
                request.ChildIds.Contains(endorsement.Id) &&
                EF.Property<Guid?>(
                    endorsement,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    endorsement =>
                        EF.Property<Guid?>(
                            endorsement,
                            "Document_Id"),
                    (Guid?)null));
    }


    public async Task AddToBeneficiariesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Beneficiarys
            .Where(beneficiary =>
                request.ChildIds.Contains(beneficiary.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    beneficiary =>
                        EF.Property<Guid?>(
                            beneficiary,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromBeneficiariesAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Beneficiarys
            .Where(beneficiary =>
                request.ChildIds.Contains(beneficiary.Id) &&
                EF.Property<Guid?>(
                    beneficiary,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    beneficiary =>
                        EF.Property<Guid?>(
                            beneficiary,
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
