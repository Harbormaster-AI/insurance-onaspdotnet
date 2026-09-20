using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IReinsuranceAgreementService {

    Task Create(ReinsuranceAgreement model , CancellationToken cancellationToken);
    Task<bool> Update(ReinsuranceAgreement model, CancellationToken cancellationToken);
    Task<ReinsuranceAgreement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ReinsuranceAgreement>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInsurer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInsurer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ReinsuranceAgreementService : IReinsuranceAgreementService
{
    private readonly IReinsuranceAgreementRepository _repository;
    private readonly ILogger<ReinsuranceAgreementService> _logger;

    public ReinsuranceAgreementService(
        IReinsuranceAgreementRepository repository, ILogger<ReinsuranceAgreementService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(ReinsuranceAgreement model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(ReinsuranceAgreement model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.AgreementNumber = model.AgreementNumber;
            existing.EffectivePeriod = model.EffectivePeriod;
            existing.Retention = model.Retention;
            existing.Limit = model.Limit;
            existing.CessionPercentage = model.CessionPercentage;
            existing.ReinsuranceType = model.ReinsuranceType;
            existing.TreatyType = model.TreatyType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<ReinsuranceAgreement?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ReinsuranceAgreement>> GetAll(CancellationToken cancellationToken)
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

    public async Task<bool> AssignInsurer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> UnassignInsurer(AssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }


    public async Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
