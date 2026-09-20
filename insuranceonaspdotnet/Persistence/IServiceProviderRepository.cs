using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IServiceProviderRepository
{
    Task<ServiceProvider?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ServiceProvider>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ServiceProvider serviceProvider, CancellationToken cancellationToken);
    Task UpdateAsync(ServiceProvider serviceProvider, CancellationToken cancellationToken);
    Task DeleteAsync(ServiceProvider serviceProvider, CancellationToken cancellationToken);
}
