
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface IPolicyService {

    Task Create(Policy model , CancellationToken cancellationToken);
    Task<bool> Update(Policy model, CancellationToken cancellationToken);
    Task<Policy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Policy>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------
    Task<bool> AssignInsurer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignInsurer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignCustomer(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignProduct(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignProduct(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignAgent(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignAgent(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AssignBillingAccount(AssociationRequest request, CancellationToken cancellationToken);
    Task<bool> UnassignBillingAccount(AssociationRequest request, CancellationToken cancellationToken);

    Task<bool> AddToCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromCoverages(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToEndorsements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromEndorsements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBeneficiaries(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBeneficiaries(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class PolicyService : IPolicyService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly IPolicyRepository _repository;
    private readonly ILogger<PolicyService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public PolicyService(
        ApplicationTelemetry telemetry,
        IPolicyRepository repository,
        ILogger<PolicyService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Policy model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Policy",
                "CreatePolicy",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Policy model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.PolicyNumber = model.PolicyNumber;
            existing.EffectivePeriod = model.EffectivePeriod;
            existing.TotalPremium = model.TotalPremium;
            existing.Status = model.Status;
            existing.PaymentPlan = model.PaymentPlan;

            await _telemetry.Execute(
                "Policy",
                "UpdatePolicy",
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

    public Task<Policy?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Policy>> GetAll(CancellationToken cancellationToken)
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
                "Policy",
                "UpdatePolicy",
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
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignCustomer(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
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
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
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

    public async Task<bool> AssignProduct(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<InsuranceProductService>().Get(childRequest, cancellationToken);
            parent.Product = child;
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

    public async Task<bool> UnassignProduct(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Product = null;
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

    public async Task<bool> AssignAgent(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<AgentService>().Get(childRequest, cancellationToken);
            parent.Agent = child;
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

    public async Task<bool> UnassignAgent(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.Agent = null;
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

    public async Task<bool> AssignBillingAccount(AssociationRequest request, CancellationToken cancellationToken) {

        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            var childRequest = new IdentifierRequest
            {
                Id = request.ChildId,
            };

            var child = await _serviceResolver.Get<BillingAccountService>().Get(childRequest, cancellationToken);
            parent.BillingAccount = child;
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

    public async Task<bool> UnassignBillingAccount(AssociationRequest request, CancellationToken cancellationToken) {
        var parent = await _repository.GetByIdAsync(request.ParentId, cancellationToken);
        if (parent is null)
        {
            _logger.LogError("No Policy found using Id {ParentId}", request.ParentId);
            return false;
        }

        try
        {
            parent.BillingAccount = null;
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
                "Policy",
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
                "Policy",
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

    public async Task<bool> AddToInsuredObjects(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
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
                "Policy",
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

    public async Task<bool> AddToEndorsements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
                "AddToEndorsements",
                () => _repository.AddToEndorsementsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromEndorsements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
                "RemoveFromEndorsements",
                () => _repository.RemoveFromEndorsementsAsync(request, cancellationToken));
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

    public async Task<bool> AddToBeneficiaries(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
                "AddToBeneficiaries",
                () => _repository.AddToBeneficiariesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromBeneficiaries(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
                "RemoveFromBeneficiaries",
                () => _repository.RemoveFromBeneficiariesAsync(request, cancellationToken));
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
                "Policy",
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
                "Policy",
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

    public async Task<bool> AddToReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
                "AddToReinsuranceAgreements",
                () => _repository.AddToReinsuranceAgreementsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromReinsuranceAgreements(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Policy",
                "RemoveFromReinsuranceAgreements",
                () => _repository.RemoveFromReinsuranceAgreementsAsync(request, cancellationToken));
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
