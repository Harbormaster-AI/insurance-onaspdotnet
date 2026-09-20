using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IDistributorRepository
{
    Task<Distributor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Distributor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Distributor distributor, CancellationToken cancellationToken);
    Task UpdateAsync(Distributor distributor, CancellationToken cancellationToken);
    Task DeleteAsync(Distributor distributor, CancellationToken cancellationToken);
}
