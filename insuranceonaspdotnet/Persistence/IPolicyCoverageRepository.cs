using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IPolicyCoverageRepository
{
    Task<PolicyCoverage?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<PolicyCoverage>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(PolicyCoverage policyCoverage, CancellationToken cancellationToken);
    Task UpdateAsync(PolicyCoverage policyCoverage, CancellationToken cancellationToken);
    Task DeleteAsync(PolicyCoverage policyCoverage, CancellationToken cancellationToken);
}
