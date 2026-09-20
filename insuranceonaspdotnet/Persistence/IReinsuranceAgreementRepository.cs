using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IReinsuranceAgreementRepository
{
    Task<ReinsuranceAgreement?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ReinsuranceAgreement>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ReinsuranceAgreement reinsuranceAgreement, CancellationToken cancellationToken);
    Task UpdateAsync(ReinsuranceAgreement reinsuranceAgreement, CancellationToken cancellationToken);
    Task DeleteAsync(ReinsuranceAgreement reinsuranceAgreement, CancellationToken cancellationToken);
}
