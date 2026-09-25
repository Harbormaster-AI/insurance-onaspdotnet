using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IExposureRepository
{
    Task<Exposure?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Exposure>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Exposure exposure, CancellationToken cancellationToken);
    Task UpdateAsync(Exposure exposure, CancellationToken cancellationToken);
    Task DeleteAsync(Exposure exposure, CancellationToken cancellationToken);

    Task AddToReservesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromReservesAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task AddToPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task RemoveFromPaymentsAsync( MultipleAssociationRequest request, CancellationToken cancellationToken);

}
