
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Domain;

using Microsoft.EntityFrameworkCore;

namespace insuranceonaspdotnet.Persistence;

public class AgentRepository : IAgentRepository
{
    private readonly ApplicationDbContext _db;

    public AgentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Agent?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _db.Agents
            .Include(x => x.Distributor)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Agent>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Agents
            .AsNoTracking()
            .Include(x => x.Distributor)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Agent agent, CancellationToken cancellationToken)
    {
        _db.Agents.Add(agent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Agent agent, CancellationToken cancellationToken)
    {
        _db.Agents.Update(agent);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Agent agent, CancellationToken cancellationToken)
    {
        _db.Agents.Remove(agent);
        await _db.SaveChangesAsync(cancellationToken);
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


    public async Task AddToCustomersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Customers
            .Where(customer =>
                request.ChildIds.Contains(customer.Id))
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customer =>
                        EF.Property<Guid?>(
                            customer,
                            "Document_Id"),
                    request.ParentId));
    }

    public async Task RemoveFromCustomersAsync(
        MultipleAssociationRequest request,
        CancellationToken cancellationToken)
    {
        await _db.Customers
            .Where(customer =>
                request.ChildIds.Contains(customer.Id) &&
                EF.Property<Guid?>(
                    customer,
                    "Document_Id") == request.ParentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(
                    customer =>
                        EF.Property<Guid?>(
                            customer,
                            "Document_Id"),
                    (Guid?)null));
    }

}
