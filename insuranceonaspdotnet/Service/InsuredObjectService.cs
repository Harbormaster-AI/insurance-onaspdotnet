
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IInsuredObjectService {

    Task Create(InsuredObject model , CancellationToken cancellationToken);
    Task<bool> Update(InsuredObject model, CancellationToken cancellationToken);
    Task<InsuredObject?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<InsuredObject>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignPolicy(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignPolicy(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class InsuredObjectService : IInsuredObjectService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IInsuredObjectRepository _repository;
    private readonly ILogger<InsuredObjectService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public InsuredObjectService(
        ApplicationTelemetry telemetry,
        IInsuredObjectRepository repository,
        ILogger<InsuredObjectService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(InsuredObject model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "InsuredObject",
                "CreateInsuredObject",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(InsuredObject model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Description = model.Description;
            existing.SerialOrId = model.SerialOrId;
            existing.PrimaryAddress = model.PrimaryAddress;
            existing.ObjectType = model.ObjectType;

            await _telemetry.Execute(
                "InsuredObject",
                "UpdateInsuredObject",
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

    public Task<InsuredObject?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<InsuredObject>> GetAll(CancellationToken cancellationToken)
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
                "InsuredObject",
                "UpdateInsuredObject",
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
            _logger.LogError("No InsuredObject found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No InsuredObject found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsuredObject",
                "AddToCoverages",
                () => _repository.AddToCoveragesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "InsuredObject",
                "RemoveFromCoverages",
                () => _repository.RemoveFromCoveragesAsync(request, cancellationToken));
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
