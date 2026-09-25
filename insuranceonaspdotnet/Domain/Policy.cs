
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Policy
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PolicyId { get; set; } 
 public virtual PolicyNumber? PolicyNumber { get; set; } 
 public virtual DateRange? EffectivePeriod { get; set; } 
 public virtual Money? TotalPremium { get; set; } 
public virtual Insurer? Insurer { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual InsuranceProduct? Product { get; set; } 
public virtual Agent? Agent { get; set; } 
public virtual ICollection<PolicyCoverage> Coverages { get; set; } = new List<PolicyCoverage>();
public virtual ICollection<InsuredObject> InsuredObjects { get; set; } = new List<InsuredObject>();
public virtual ICollection<Endorsement> Endorsements { get; set; } = new List<Endorsement>();
public virtual BillingAccount? BillingAccount { get; set; } 
public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
public virtual ICollection<ReinsuranceAgreement> ReinsuranceAgreements { get; set; } = new List<ReinsuranceAgreement>();
 public virtual PolicyStatus? Status { get; set; } 
 public virtual PaymentPlanType? PaymentPlan { get; set; } 

    public static Policy FromRequest(PolicyRequest request) {
        return new Policy {
            Id = request.Id,
            PolicyNumber = request.PolicyNumber,
            EffectivePeriod = request.EffectivePeriod,
            TotalPremium = request.TotalPremium,
            Status = request.Status,
            PaymentPlan = request.PaymentPlan,
        };
    }
}
