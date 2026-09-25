using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IInsuredObjectRepository
{
    Task<InsuredObject?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsuredObject>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsuredObject insuredObject, CancellationToken cancellationToken);
    Task UpdateAsync(InsuredObject insuredObject, CancellationToken cancellationToken);
    Task DeleteAsync(InsuredObject insuredObject, CancellationToken cancellationToken);

    Task AddToCoveragesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromCoveragesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
