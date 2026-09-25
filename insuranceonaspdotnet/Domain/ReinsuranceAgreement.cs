
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class ReinsuranceAgreement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ReinsuranceagreementId { get; set; } 
 public virtual string? AgreementNumber { get; set; } 
 public virtual DateRange? EffectivePeriod { get; set; } 
 public virtual Money? Retention { get; set; } 
 public virtual Money? Limit { get; set; } 
 public virtual Percentage? CessionPercentage { get; set; } 
public virtual Insurer? Insurer { get; set; } 
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
 public virtual ReinsuranceType? ReinsuranceType { get; set; } 
 public virtual TreatyType? TreatyType { get; set; } 

    public static ReinsuranceAgreement FromRequest(ReinsuranceAgreementRequest request) {
        return new ReinsuranceAgreement {
            Id = request.Id,
            AgreementNumber = request.AgreementNumber,
            EffectivePeriod = request.EffectivePeriod,
            Retention = request.Retention,
            Limit = request.Limit,
            CessionPercentage = request.CessionPercentage,
            ReinsuranceType = request.ReinsuranceType,
            TreatyType = request.TreatyType,
        };
    }
}
