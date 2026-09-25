using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IDistributorRepository
{
    Task<Distributor?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Distributor>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Distributor distributor, CancellationToken cancellationToken);
    Task UpdateAsync(Distributor distributor, CancellationToken cancellationToken);
    Task DeleteAsync(Distributor distributor, CancellationToken cancellationToken);

    Task AddToInsurersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInsurersAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAgentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAgentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
