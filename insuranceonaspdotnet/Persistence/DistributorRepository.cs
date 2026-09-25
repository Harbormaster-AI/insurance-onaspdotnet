
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class DistributorRepository : IDistributorRepository
{
    private readonly ApplicationDbContext _db;

    public DistributorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Distributor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Distributors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Distributor>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Distributors
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Distributor distributor, CancellationToken cancellationToken)
    {
        _db.Distributors.Add(distributor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Distributor distributor, CancellationToken cancellationToken)
    {
        _db.Distributors.Update(distributor);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Distributor distributor, CancellationToken cancellationToken)
    {
        _db.Distributors.Remove(distributor);
        await _db.SaveChangesAsync(cancellationToken);
    }


    public async Task AddToInsurersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Insurers
            .Where(insurer =>
                request.ChildIds.Contains(insurer.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insurer =>
                        EF.Property<Guid?>(
                            insurer,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromInsurersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Insurers
            .Where(insurer =>
                request.ChildIds.Contains(insurer.Id) &&
                EF.Property<Guid?>(
                    insurer,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    insurer =>
                        EF.Property<Guid?>(
                            insurer,
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

}
