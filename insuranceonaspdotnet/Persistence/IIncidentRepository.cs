using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IIncidentRepository
{
    Task<Incident?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Incident>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Incident incident, CancellationToken cancellationToken);
    Task UpdateAsync(Incident incident, CancellationToken cancellationToken);
    Task DeleteAsync(Incident incident, CancellationToken cancellationToken);

    Task AddToInsuredObjectsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromInsuredObjectsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
