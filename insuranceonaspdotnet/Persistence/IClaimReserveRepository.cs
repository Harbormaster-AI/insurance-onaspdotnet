using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IClaimReserveRepository
{
    Task<ClaimReserve?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClaimReserve>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ClaimReserve claimReserve, CancellationToken cancellationToken);
    Task UpdateAsync(ClaimReserve claimReserve, CancellationToken cancellationToken);
    Task DeleteAsync(ClaimReserve claimReserve, CancellationToken cancellationToken);
}
