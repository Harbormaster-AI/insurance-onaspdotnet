using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IPolicyCoverageService {

    Task Create(PolicyCoverage model , CancellationToken cancellationToken);
    Task<bool> Update(PolicyCoverage model, CancellationToken cancellationToken);
    Task<PolicyCoverage?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<PolicyCoverage>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PolicyCoverageService : IPolicyCoverageService
{
    private readonly IPolicyCoverageRepository _repository;
    private readonly ILogger<PolicyCoverageService> _logger;

    public PolicyCoverageService(
        IPolicyCoverageRepository repository, ILogger<PolicyCoverageService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(PolicyCoverage model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(PolicyCoverage model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Limit = model.Limit;
            existing.Deductible = model.Deductible;
            existing.Premium = model.Premium;
            existing.CoverageType = model.CoverageType;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<PolicyCoverage?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<PolicyCoverage>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
