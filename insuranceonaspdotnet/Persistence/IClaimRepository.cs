using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Claim>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Claim claim, CancellationToken cancellationToken);
    Task UpdateAsync(Claim claim, CancellationToken cancellationToken);
    Task DeleteAsync(Claim claim, CancellationToken cancellationToken);
}
