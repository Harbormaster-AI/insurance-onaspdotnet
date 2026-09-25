
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Insurer
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InsurerId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? DomicileCountry { get; set; } 
 public virtual string? NaicNumber { get; set; } 
 public virtual string? Website { get; set; } 
public virtual ICollection<InsuranceProduct> Products { get; set; } = new List<InsuranceProduct>();
public virtual ICollection<Distributor> DistributionPartners { get; set; } = new List<Distributor>();
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
public virtual ICollection<ReinsuranceAgreement> ReinsuranceAgreements { get; set; } = new List<ReinsuranceAgreement>();

    public static Insurer FromRequest(InsurerRequest request) {
        return new Insurer {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            DomicileCountry = request.DomicileCountry,
            NaicNumber = request.NaicNumber,
            Website = request.Website,
        };
    }
}
