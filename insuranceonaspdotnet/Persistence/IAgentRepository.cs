using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IAgentRepository
{
    Task<Agent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Agent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Agent agent, CancellationToken cancellationToken);
    Task UpdateAsync(Agent agent, CancellationToken cancellationToken);
    Task DeleteAsync(Agent agent, CancellationToken cancellationToken);

    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToCustomersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCustomersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
