
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPolicyCoverageRepository _repository;
    private readonly ILogger<PolicyCoverageService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PolicyCoverageService(
        ApplicationTelemetry telemetry,
        IPolicyCoverageRepository repository,
        ILogger<PolicyCoverageService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(PolicyCoverage model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "PolicyCoverage",
                "CreatePolicyCoverage",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "PolicyCoverage",
                "UpdatePolicyCoverage",
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
            await _telemetry.Execute(
                "PolicyCoverage",
                "UpdatePolicyCoverage",
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

    public async Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PolicyCoverage found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<PolicyService>().Get(childRequest, cancellationToken);
            parent.Policy = child;
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

    public async Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No PolicyCoverage found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Policy = null;
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


    public async Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "PolicyCoverage",
                "AddToInsuredObjects",
                () => _repository.AddToInsuredObjectsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "PolicyCoverage",
                "RemoveFromInsuredObjects",
                () => _repository.RemoveFromInsuredObjectsAsync(request, cancellationToken));
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
