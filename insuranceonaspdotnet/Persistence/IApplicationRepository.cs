using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IApplicationRepository
{
    Task<Application?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Application>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Application application, CancellationToken cancellationToken);
    Task UpdateAsync(Application application, CancellationToken cancellationToken);
    Task DeleteAsync(Application application, CancellationToken cancellationToken);

    Task AddToQuotesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromQuotesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
