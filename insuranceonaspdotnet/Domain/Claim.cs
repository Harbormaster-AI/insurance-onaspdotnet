
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Claim
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ClaimId { get; set; } 
 public virtual ClaimNumber? ClaimNumber { get; set; } 
 public virtual DateOnly? NoticeDate { get; set; } 
 public virtual DateOnly? LossDate { get; set; } 
 public virtual string? ReportedBy { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Adjuster? Adjuster { get; set; } 
public virtual Incident? Incident { get; set; } 
public virtual ICollection<Exposure> Exposures { get; set; } = new List<Exposure>();
public virtual ICollection<ClaimReserve> Reserves { get; set; } = new List<ClaimReserve>();
public virtual ICollection<ClaimPayment> ClaimPayments { get; set; } = new List<ClaimPayment>();
public virtual ICollection<ServiceProvider_> ServiceProviders { get; set; } = new List<ServiceProvider_>();
public virtual ICollection<SubrogationRecovery> Subrogations { get; set; } = new List<SubrogationRecovery>();
 public virtual ClaimStatus? Status { get; set; } 
 public virtual CauseOfLoss? LossCause { get; set; } 

    public static Claim FromRequest(ClaimRequest request) {
        return new Claim {
            Id = request.Id,
            ClaimNumber = request.ClaimNumber,
            NoticeDate = request.NoticeDate,
            LossDate = request.LossDate,
            ReportedBy = request.ReportedBy,
            Status = request.Status,
            LossCause = request.LossCause,
        };
    }
}
