
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInsuranceProductRepository _repository;
    private readonly ILogger<InsuranceProductService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InsuranceProductService(
        ApplicationTelemetry telemetry,
        IInsuranceProductRepository repository,
        ILogger<InsuranceProductService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InsuranceProduct model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InsuranceProduct",
                "CreateInsuranceProduct",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "InsuranceProduct",
                "UpdateInsuranceProduct",
                () => _repository.UpdateAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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
            await _telemetry.Execute(
                "InsuranceProduct",
                "UpdateInsuranceProduct",
                () => _repository.DeleteAsync(existing, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> AssignInsurer(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InsuranceProduct found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<InsurerService>().Get(childRequest, cancellationToken);
            parent.Insurer = child;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> UnassignInsurer(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No InsuranceProduct found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Insurer = null;
            await Update( parent, cancellationToken );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }


    public async Task<bool> AddToCoverageDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsuranceProduct",
                "AddToCoverageDefinitions",
                () => _repository.AddToCoverageDefinitionsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
           _logger.LogError(
                   ex,
                   "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveFromCoverageDefinitions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsuranceProduct",
                "RemoveFromCoverageDefinitions",
                () => _repository.RemoveFromCoverageDefinitionsAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
            return false;
        }
        return true;
    }



}
