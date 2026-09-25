using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface ICoverageDefinitionRepository
{
    Task<CoverageDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<CoverageDefinition>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(CoverageDefinition coverageDefinition, CancellationToken cancellationToken);
    Task UpdateAsync(CoverageDefinition coverageDefinition, CancellationToken cancellationToken);
    Task DeleteAsync(CoverageDefinition coverageDefinition, CancellationToken cancellationToken);


}
