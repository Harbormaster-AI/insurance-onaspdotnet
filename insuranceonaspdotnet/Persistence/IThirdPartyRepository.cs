using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public interface IThirdPartyRepository
{
    Task<ThirdParty?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThirdParty>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(ThirdParty thirdParty, CancellationToken cancellationToken);
    Task UpdateAsync(ThirdParty thirdParty, CancellationToken cancellationToken);
    Task DeleteAsync(ThirdParty thirdParty, CancellationToken cancellationToken);
}
