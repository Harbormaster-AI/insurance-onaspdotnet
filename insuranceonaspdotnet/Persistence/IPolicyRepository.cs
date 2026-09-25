using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IPolicyRepository
{
    Task<Policy?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Policy>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Policy policy, CancellationToken cancellationToken);
    Task UpdateAsync(Policy policy, CancellationToken cancellationToken);
    Task DeleteAsync(Policy policy, CancellationToken cancellationToken);

    Task AddToCoveragesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCoveragesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToInsuredObjectsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInsuredObjectsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToEndorsementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromEndorsementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBeneficiariesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBeneficiariesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReinsuranceAgreementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReinsuranceAgreementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
