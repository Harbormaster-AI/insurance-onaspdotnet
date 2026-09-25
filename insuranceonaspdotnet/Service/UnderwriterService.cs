
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IUnderwriterService {

    Task Create(Underwriter model , CancellationToken cancellationToken);
    Task<bool> Update(Underwriter model, CancellationToken cancellationToken);
    Task<Underwriter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Underwriter>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInsurer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInsurer(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class UnderwriterService : IUnderwriterService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IUnderwriterRepository _repository;
    private readonly ILogger<UnderwriterService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public UnderwriterService(
        ApplicationTelemetry telemetry,
        IUnderwriterRepository repository,
        ILogger<UnderwriterService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Underwriter model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Underwriter",
                "CreateUnderwriter",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Underwriter model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.EmployeeId = model.EmployeeId;
            existing.AuthorityLimit = model.AuthorityLimit;

            await _telemetry.Execute(
                "Underwriter",
                "UpdateUnderwriter",
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

    public Task<Underwriter?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Underwriter>> GetAll(CancellationToken cancellationToken)
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
                "Underwriter",
                "UpdateUnderwriter",
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
            _logger.LogError("No Underwriter found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Underwriter found using Id {ParentId}", request.ParentId);
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


    public async Task<bool> AddToDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Underwriter",
                "AddToDecisions",
                () => _repository.AddToDecisionsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromDecisions(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Underwriter",
                "RemoveFromDecisions",
                () => _repository.RemoveFromDecisionsAsync(request, cancellationToken));
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
