using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IAdjusterRepository
{
    Task<Adjuster?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Adjuster>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Adjuster adjuster, CancellationToken cancellationToken);
    Task UpdateAsync(Adjuster adjuster, CancellationToken cancellationToken);
    Task DeleteAsync(Adjuster adjuster, CancellationToken cancellationToken);
}
