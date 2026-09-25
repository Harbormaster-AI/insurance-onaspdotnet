
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IAdjusterService {

    Task Create(Adjuster model , CancellationToken cancellationToken);
    Task<bool> Update(Adjuster model, CancellationToken cancellationToken);
    Task<Adjuster?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Adjuster>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class AdjusterService : IAdjusterService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IAdjusterRepository _repository;
    private readonly ILogger<AdjusterService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public AdjusterService(
        ApplicationTelemetry telemetry,
        IAdjusterRepository repository,
        ILogger<AdjusterService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Adjuster model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Adjuster",
                "CreateAdjuster",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Adjuster model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.LicenseNumber = model.LicenseNumber;
            existing.AdjusterType = model.AdjusterType;

            await _telemetry.Execute(
                "Adjuster",
                "UpdateAdjuster",
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

    public Task<Adjuster?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Adjuster>> GetAll(CancellationToken cancellationToken)
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
                "Adjuster",
                "UpdateAdjuster",
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


    public async Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Adjuster",
                "AddToClaims",
                () => _repository.AddToClaimsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Adjuster",
                "RemoveFromClaims",
                () => _repository.RemoveFromClaimsAsync(request, cancellationToken));
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

    public async Task<bool> AddToServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Adjuster",
                "AddToServiceProviders",
                () => _repository.AddToServiceProvidersAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromServiceProviders(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Adjuster",
                "RemoveFromServiceProviders",
                () => _repository.RemoveFromServiceProvidersAsync(request, cancellationToken));
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
