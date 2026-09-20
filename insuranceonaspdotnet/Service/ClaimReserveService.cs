using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IClaimReserveService {

    Task Create(ClaimReserve model , CancellationToken cancellationToken);
    Task<bool> Update(ClaimReserve model, CancellationToken cancellationToken);
    Task<ClaimReserve?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ClaimReserve>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignClaim(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignClaim(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignExposure(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignExposure(AssociationRequest request, CancellationToken cancellationToken);


}

public class ClaimReserveService : IClaimReserveService
{
    private readonly IClaimReserveRepository _repository;
    private readonly ILogger<ClaimReserveService> _logger;

    public ClaimReserveService(
        IClaimReserveRepository repository, ILogger<ClaimReserveService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ClaimReserve model, CancellationToken cancellationToken)
    {

 
         try
        {
            await _repository.AddAsync(model, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
        }
    }

    public async Task<bool> Update(ClaimReserve model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Amount = model.Amount;
            existing.SetDate = model.SetDate;
            existing.ReserveType = model.ReserveType;
            existing.Status = model.Status;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ClaimReserve?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ClaimReserve>> GetAll(CancellationToken cancellationToken)
    => _repository.GetAllAsync(cancellationToken);

    public async Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(identifier.Id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        try
        {
            await _repository.DeleteAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;

    }

    public async Task<bool> AssignClaim(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignClaim(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignExposure(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignExposure(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
