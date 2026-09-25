using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IUnderwritingDecisionRepository
{
    Task<UnderwritingDecision?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<UnderwritingDecision>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(UnderwritingDecision underwritingDecision, CancellationToken cancellationToken);
    Task UpdateAsync(UnderwritingDecision underwritingDecision, CancellationToken cancellationToken);
    Task DeleteAsync(UnderwritingDecision underwritingDecision, CancellationToken cancellationToken);


}
