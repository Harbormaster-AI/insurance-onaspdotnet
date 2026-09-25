using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Claim>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Claim claim, CancellationToken cancellationToken);
    Task UpdateAsync(Claim claim, CancellationToken cancellationToken);
    Task DeleteAsync(Claim claim, CancellationToken cancellationToken);

    Task AddToExposuresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromExposuresAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReservesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReservesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToClaimPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToServiceProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromServiceProvidersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToSubrogationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromSubrogationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
