
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IThirdPartyService {

    Task Create(ThirdParty model , CancellationToken cancellationToken);
    Task<bool> Update(ThirdParty model, CancellationToken cancellationToken);
    Task<ThirdParty?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<ThirdParty>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class ThirdPartyService : IThirdPartyService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IThirdPartyRepository _repository;
    private readonly ILogger<ThirdPartyService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ThirdPartyService(
        ApplicationTelemetry telemetry,
        IThirdPartyRepository repository,
        ILogger<ThirdPartyService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(ThirdParty model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "ThirdParty",
                "CreateThirdParty",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(ThirdParty model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.Name = model.Name;
            existing.TaxId = model.TaxId;
            existing.Address = model.Address;
            existing.PartyType = model.PartyType;

            await _telemetry.Execute(
                "ThirdParty",
                "UpdateThirdParty",
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

    public Task<ThirdParty?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<ThirdParty>> GetAll(CancellationToken cancellationToken)
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
                "ThirdParty",
                "UpdateThirdParty",
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


    public async Task<bool> AddToSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "AddToSubrogations",
                () => _repository.AddToSubrogationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "ThirdParty",
                "RemoveFromSubrogations",
                () => _repository.RemoveFromSubrogationsAsync(request, cancellationToken));
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
