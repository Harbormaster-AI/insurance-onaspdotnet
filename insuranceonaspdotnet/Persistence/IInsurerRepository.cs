using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IInsurerRepository
{
    Task<Insurer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Insurer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Insurer insurer, CancellationToken cancellationToken);
    Task UpdateAsync(Insurer insurer, CancellationToken cancellationToken);
    Task DeleteAsync(Insurer insurer, CancellationToken cancellationToken);
}
