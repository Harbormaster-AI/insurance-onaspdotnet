using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Service;

public interface IInsuranceProductService {

    Task Create(InsuranceProduct model , CancellationToken cancellationToken);
    Task<bool> Update(InsuranceProduct model, CancellationToken cancellationToken);
    Task<InsuranceProduct?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsuranceProduct>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);

    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInsurer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInsurer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCoverageDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCoverageDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InsuranceProductService : IInsuranceProductService
{
    private readonly IInsuranceProductRepository _repository;
    private readonly ILogger<InsuranceProductService> _logger;

    public InsuranceProductService(
        IInsuranceProductRepository repository, ILogger<InsuranceProductService> logger )
    {
        _repository = repository;
        _logger = logger;
    }


    public async Task Create(InsuranceProduct model, CancellationToken cancellationToken)
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

    public async Task<bool> Update(InsuranceProduct model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.ProductCode = model.ProductCode;
            existing.LineOfBusiness = model.LineOfBusiness;

            await _repository.UpdateAsync(existing, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unexpected Error: {ex.Message}");
            return false;
        }
        return true;
    }

    public Task<InsuranceProduct?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InsuranceProduct>> GetAll(CancellationToken cancellationToken)
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


    public async Task<bool> AddToCoverageDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }
    public async Task<bool> RemoveFromCoverageDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        return true;
    }



}
