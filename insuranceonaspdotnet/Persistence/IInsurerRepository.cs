using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IInsurerRepository
{
    Task<Insurer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Insurer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Insurer insurer, CancellationToken cancellationToken);
    Task UpdateAsync(Insurer insurer, CancellationToken cancellationToken);
    Task DeleteAsync(Insurer insurer, CancellationToken cancellationToken);

    Task AddToProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromProductsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToDistributionPartnersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromDistributionPartnersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToReinsuranceAgreementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReinsuranceAgreementsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
