using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Persistence;

public interface IBeneficiaryRepository
{
    Task<Beneficiary?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Beneficiary>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Beneficiary beneficiary, CancellationToken cancellationToken);
    Task UpdateAsync(Beneficiary beneficiary, CancellationToken cancellationToken);
    Task DeleteAsync(Beneficiary beneficiary, CancellationToken cancellationToken);


}
