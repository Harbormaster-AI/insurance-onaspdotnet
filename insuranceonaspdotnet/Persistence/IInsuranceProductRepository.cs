using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IInsuranceProductRepository
{
    Task<InsuranceProduct?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsuranceProduct>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsuranceProduct insuranceProduct, CancellationToken cancellationToken);
    Task UpdateAsync(InsuranceProduct insuranceProduct, CancellationToken cancellationToken);
    Task DeleteAsync(InsuranceProduct insuranceProduct, CancellationToken cancellationToken);

    Task AddToCoverageDefinitionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCoverageDefinitionsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
