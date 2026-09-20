using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Customer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CustomerId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? OrganizationName { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual DateOnly? DateOfBirth { get; set; } 
 public virtual Address? PrimaryAddress { get; set; } 
public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
 public virtual CustomerType? CustomerType { get; set; } 

    public static Customer FromRequest(CustomerRequest request) {
        return new Customer {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            OrganizationName = request.OrganizationName,
            TaxId = request.TaxId,
            DateOfBirth = request.DateOfBirth,
            PrimaryAddress = request.PrimaryAddress,
            CustomerType = request.CustomerType,
        };
    }
}
