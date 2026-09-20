using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Exposure
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ExposureId { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual PolicyCoverage? PolicyCoverage { get; set; } 
public virtual InsuredObject? InsuredObject { get; set; } 
public virtual ICollection<ClaimReserve> Reserves { get; set; } = new List<ClaimReserve>();
public virtual ICollection<ClaimPayment> Payments { get; set; } = new List<ClaimPayment>();
 public virtual ExposureType? ExposureType { get; set; } 
 public virtual ExposureStatus? Status { get; set; } 

    public static Exposure FromRequest(ExposureRequest request) {
        return new Exposure {
            Id = request.Id,
            ExposureType = request.ExposureType,
            Status = request.Status,
        };
    }
}
