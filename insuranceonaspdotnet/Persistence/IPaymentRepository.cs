using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Payment payment, CancellationToken cancellationToken);
    Task UpdateAsync(Payment payment, CancellationToken cancellationToken);
    Task DeleteAsync(Payment payment, CancellationToken cancellationToken);
}
