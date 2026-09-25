
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class CustomerRepository : ICustomerRepository
{
    private readonly ApplicationDbContext _db;

    public CustomerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Customers
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Customers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Update(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken)
    {
        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Applications
            .Where(application =>
                request.ChildIds.Contains(application.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    application =>
                        EF.Property<Guid?>(
                            application,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromApplicationsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Applications
            .Where(application =>
                request.ChildIds.Contains(application.Id) &&
                EF.Property<Guid?>(
                    application,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    application =>
                        EF.Property<Guid?>(
                            application,
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


    public async Task AddToAgentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Agents
            .Where(agent =>
                request.ChildIds.Contains(agent.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    agent =>
                        EF.Property<Guid?>(
                            agent,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromAgentsAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Agents
            .Where(agent =>
                request.ChildIds.Contains(agent.Id) &&
                EF.Property<Guid?>(
                    agent,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    agent =>
                        EF.Property<Guid?>(
                            agent,
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

}
