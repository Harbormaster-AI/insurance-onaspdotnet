using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IEndorsementRepository
{
    Task<Endorsement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Endorsement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Endorsement endorsement, CancellationToken cancellationToken);
    Task UpdateAsync(Endorsement endorsement, CancellationToken cancellationToken);
    Task DeleteAsync(Endorsement endorsement, CancellationToken cancellationToken);
}
