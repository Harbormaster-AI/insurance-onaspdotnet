
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

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
    private readonly ApplicationTelemetry _telemetry;
    private readonly IClaimRepository _repository;
    private readonly ILogger<ClaimService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public ClaimService(
        ApplicationTelemetry telemetry,
        IClaimRepository repository,
        ILogger<ClaimService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Claim model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Claim",
                "CreateClaim",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
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

            await _telemetry.Execute(
                "Claim",
                "UpdateClaim",
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
            await _telemetry.Execute(
                "Claim",
                "UpdateClaim",
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
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<CustomerService>().Get(childRequest, cancellationToken);
            parent.Customer = child;
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

    public async Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Customer = null;
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

    public async Task<bool> AssignAdjuster(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AdjusterService>().Get(childRequest, cancellationToken);
            parent.Adjuster = child;
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

    public async Task<bool> UnassignAdjuster(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Adjuster = null;
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

    public async Task<bool> AssignIncident(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<IncidentService>().Get(childRequest, cancellationToken);
            parent.Incident = child;
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

    public async Task<bool> UnassignIncident(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Claim found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Incident = null;
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


    public async Task<bool> AddToExposures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
                "AddToExposures",
                () => _repository.AddToExposuresAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromExposures(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
                "RemoveFromExposures",
                () => _repository.RemoveFromExposuresAsync(request, cancellationToken));
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

    public async Task<bool> AddToReserves(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
                "AddToReserves",
                () => _repository.AddToReservesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromReserves(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
                "RemoveFromReserves",
                () => _repository.RemoveFromReservesAsync(request, cancellationToken));
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

    public async Task<bool> AddToClaimPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
                "AddToClaimPayments",
                () => _repository.AddToClaimPaymentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromClaimPayments(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
                "RemoveFromClaimPayments",
                () => _repository.RemoveFromClaimPaymentsAsync(request, cancellationToken));
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
                "Claim",
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
                "Claim",
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

    public async Task<bool> AddToSubrogations(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Claim",
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
                "Claim",
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
