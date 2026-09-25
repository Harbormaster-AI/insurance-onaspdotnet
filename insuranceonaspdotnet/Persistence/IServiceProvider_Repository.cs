using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IServiceProvider_Repository
{
    Task<ServiceProvider_?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ServiceProvider_>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ServiceProvider_ serviceProvider_, CancellationToken cancellationToken);
    Task UpdateAsync(ServiceProvider_ serviceProvider_, CancellationToken cancellationToken);
    Task DeleteAsync(ServiceProvider_ serviceProvider_, CancellationToken cancellationToken);

    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
