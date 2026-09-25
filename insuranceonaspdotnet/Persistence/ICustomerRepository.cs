using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Customer customer, CancellationToken cancellationToken);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteAsync(Customer customer, CancellationToken cancellationToken);

    Task AddToApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromApplicationsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPoliciesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromClaimsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToAgentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromAgentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToBeneficiariesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromBeneficiariesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
