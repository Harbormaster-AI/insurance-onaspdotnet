using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IBillingAccountRepository
{
    Task<BillingAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BillingAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BillingAccount billingAccount, CancellationToken cancellationToken);
    Task UpdateAsync(BillingAccount billingAccount, CancellationToken cancellationToken);
    Task DeleteAsync(BillingAccount billingAccount, CancellationToken cancellationToken);
}
