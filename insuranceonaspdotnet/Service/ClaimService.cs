using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IClaimService {

    Task Create(Claim model , CancellationToken cancellationToken);
    Task<bool> Update(Claim model, CancellationToken cancellationToken);
    Task<Claim?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Claim>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAdjuster(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAdjuster(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignIncident(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignIncident(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToExposures(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromExposures(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReserves(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReserves(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToClaimPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaimPayments(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ClaimService : IClaimService
{
    private readonly IClaimRepository _repository;
    private readonly ILogger<ClaimService> _logger;

    public ClaimService(
        IClaimRepository repository, ILogger<ClaimService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(Claim model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(Claim model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.ClaimNumber = model.ClaimNumber;
            existing.NoticeDate = model.NoticeDate;
            existing.LossDate = model.LossDate;
            existing.ReportedBy = model.ReportedBy;
            existing.Status = model.Status;
            existing.LossCause = model.LossCause;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<Claim?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Claim>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignAdjuster(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignAdjuster(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AssignIncident(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignIncident(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToExposures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromExposures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToReserves(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromReserves(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToClaimPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromClaimPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }

    public async Task<bool> AddToSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
