using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IClaimPaymentRepository
{
    Task<ClaimPayment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClaimPayment>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ClaimPayment claimPayment, CancellationToken cancellationToken);
    Task UpdateAsync(ClaimPayment claimPayment, CancellationToken cancellationToken);
    Task DeleteAsync(ClaimPayment claimPayment, CancellationToken cancellationToken);


}
