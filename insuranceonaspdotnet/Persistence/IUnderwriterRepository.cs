using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IUnderwriterRepository
{
    Task<Underwriter?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Underwriter>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Underwriter underwriter, CancellationToken cancellationToken);
    Task UpdateAsync(Underwriter underwriter, CancellationToken cancellationToken);
    Task DeleteAsync(Underwriter underwriter, CancellationToken cancellationToken);

    Task AddToDecisionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDecisionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
