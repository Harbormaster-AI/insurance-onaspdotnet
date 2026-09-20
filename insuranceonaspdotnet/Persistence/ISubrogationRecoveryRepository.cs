using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface ISubrogationRecoveryRepository
{
    Task<SubrogationRecovery?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<SubrogationRecovery>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(SubrogationRecovery subrogationRecovery, CancellationToken cancellationToken);
    Task UpdateAsync(SubrogationRecovery subrogationRecovery, CancellationToken cancellationToken);
    Task DeleteAsync(SubrogationRecovery subrogationRecovery, CancellationToken cancellationToken);
}
