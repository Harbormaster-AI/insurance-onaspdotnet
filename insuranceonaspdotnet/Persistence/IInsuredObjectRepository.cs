using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IInsuredObjectRepository
{
    Task<InsuredObject?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsuredObject>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(InsuredObject insuredObject, CancellationToken cancellationToken);
    Task UpdateAsync(InsuredObject insuredObject, CancellationToken cancellationToken);
    Task DeleteAsync(InsuredObject insuredObject, CancellationToken cancellationToken);
}
