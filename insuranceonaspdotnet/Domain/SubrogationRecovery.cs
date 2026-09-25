
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class SubrogationRecovery
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? SubrogationrecoveryId { get; set; } 
 public virtual string? RecoveryReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? RecoveryDate { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual Exposure? Exposure { get; set; } 
public virtual ThirdParty? Counterparty { get; set; } 
 public virtual SubrogationStatus? Status { get; set; } 

    public static SubrogationRecovery FromRequest(SubrogationRecoveryRequest request) {
        return new SubrogationRecovery {
            Id = request.Id,
            RecoveryReference = request.RecoveryReference,
            Amount = request.Amount,
            RecoveryDate = request.RecoveryDate,
            Status = request.Status,
        };
    }
}
