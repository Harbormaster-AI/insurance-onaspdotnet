using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IAdjusterRepository
{
    Task<Adjuster?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Adjuster>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Adjuster adjuster, CancellationToken cancellationToken);
    Task UpdateAsync(Adjuster adjuster, CancellationToken cancellationToken);
    Task DeleteAsync(Adjuster adjuster, CancellationToken cancellationToken);

    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToServiceProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromServiceProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
