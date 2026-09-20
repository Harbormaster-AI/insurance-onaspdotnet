using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IAgentRepository
{
    Task<Agent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Agent>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Agent agent, CancellationToken cancellationToken);
    Task UpdateAsync(Agent agent, CancellationToken cancellationToken);
    Task DeleteAsync(Agent agent, CancellationToken cancellationToken);
}
