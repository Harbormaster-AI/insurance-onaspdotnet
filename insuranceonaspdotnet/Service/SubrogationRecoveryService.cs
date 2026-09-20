using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface ISubrogationRecoveryService {

    Task Create(SubrogationRecovery model , CancellationToken cancellationToken);
    Task<bool> Update(SubrogationRecovery model, CancellationToken cancellationToken);
    Task<SubrogationRecovery?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<SubrogationRecovery>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignClaim(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignClaim(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignExposure(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignExposure(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCounterparty(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCounterparty(AssociationRequest request, CancellationToken cancellationToken);


}

public class SubrogationRecoveryService : ISubrogationRecoveryService
{
    private readonly ISubrogationRecoveryRepository _repository;
    private readonly ILogger<SubrogationRecoveryService> _logger;

    public SubrogationRecoveryService(
        ISubrogationRecoveryRepository repository, ILogger<SubrogationRecoveryService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(SubrogationRecovery model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(SubrogationRecovery model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.RecoveryReference = model.RecoveryReference;
            existing.Amount = model.Amount;
            existing.RecoveryDate = model.RecoveryDate;
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

    public Task<SubrogationRecovery?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<SubrogationRecovery>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignCounterparty(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCounterparty(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }




}
