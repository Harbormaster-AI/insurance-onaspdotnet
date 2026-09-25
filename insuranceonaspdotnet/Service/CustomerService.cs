
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Contracts;
using insuranceonaspdotnet.Telemetry;

namespace insuranceonaspdotnet.Service;

public interface ICustomerService {

    Task Create(Customer model , CancellationToken cancellationToken);
    Task<bool> Update(Customer model, CancellationToken cancellationToken);
    Task<Customer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken);
    Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken);
    Task<bool> Delete(IdentifierRequest identifier, CancellationToken cancellationToken);
    // ------------------------------
    // Single Associations
    // -------------------------------

    Task<bool> AddToApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromApplications(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromClaims(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToAgents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromAgents(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> AddToBeneficiaries(MultipleAssociationRequest request, CancellationToken cancellationToken);
    Task<bool> RemoveFromBeneficiaries(MultipleAssociationRequest request, CancellationToken cancellationToken);

}

public class CustomerService : ICustomerService
{
    private readonly ApplicationTelemetry _telemetry;
    private readonly ICustomerRepository _repository;
    private readonly ILogger<CustomerService> _logger;
    private readonly IServiceResolver _serviceResolver;


    public CustomerService(
        ApplicationTelemetry telemetry,
        ICustomerRepository repository,
        ILogger<CustomerService> logger,
        IServiceResolver serviceResolver)
    {
        _telemetry = telemetry;
        _repository = repository;
        _logger = logger;
        _serviceResolver = serviceResolver;
    }

    public async Task Create(Customer model, CancellationToken cancellationToken)
    {
        try
        {
            await _telemetry.Execute(
                "Customer",
                "CreateCustomer",
                () => _repository.AddAsync(model, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                    ex,
                    "Unexpected error while creating Transaction.");
        }
    }

    public async Task<bool> Update(Customer model, CancellationToken cancellationToken)
    {
        try {
            var existing = await _repository.GetByIdAsync(model.Id, cancellationToken);
            if (existing is null)
            {
                return false;
            }
            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.OrganizationName = model.OrganizationName;
            existing.TaxId = model.TaxId;
            existing.DateOfBirth = model.DateOfBirth;
            existing.PrimaryAddress = model.PrimaryAddress;
            existing.CustomerType = model.CustomerType;

            await _telemetry.Execute(
                "Customer",
                "UpdateCustomer",
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

    public Task<Customer?> Get(IdentifierRequest identifier, CancellationToken cancellationToken)
    => _repository.GetByIdAsync(identifier.Id, cancellationToken);

    public Task<IReadOnlyList<Customer>> GetAll(CancellationToken cancellationToken)
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
                "Customer",
                "UpdateCustomer",
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


    public async Task<bool> AddToApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToApplications",
                () => _repository.AddToApplicationsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromApplications(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromApplications",
                () => _repository.RemoveFromApplicationsAsync(request, cancellationToken));
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

    public async Task<bool> AddToPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToPolicies",
                () => _repository.AddToPoliciesAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromPolicies(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromPolicies",
                () => _repository.RemoveFromPoliciesAsync(request, cancellationToken));
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
                "Customer",
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
                "Customer",
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

    public async Task<bool> AddToAgents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "AddToAgents",
                () => _repository.AddToAgentsAsync(request, cancellationToken));
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

    public async Task<bool> RemoveFromAgents(MultipleAssociationRequest request, CancellationToken cancellationToken) {
        try {
            await _telemetry.Execute(
                "Customer",
                "RemoveFromAgents",
                () => _repository.RemoveFromAgentsAsync(request, cancellationToken));
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
                "Customer",
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
                "Customer",
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



}
