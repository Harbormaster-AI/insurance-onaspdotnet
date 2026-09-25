using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IBillingAccountRepository
{
    Task<BillingAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<BillingAccount>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(BillingAccount billingAccount, CancellationToken cancellationToken);
    Task UpdateAsync(BillingAccount billingAccount, CancellationToken cancellationToken);
    Task DeleteAsync(BillingAccount billingAccount, CancellationToken cancellationToken);

    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInvoicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInvoicesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
