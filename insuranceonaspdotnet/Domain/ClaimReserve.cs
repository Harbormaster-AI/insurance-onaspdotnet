
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class ClaimReserve
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ClaimreserveId { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? SetDate { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual Exposure? Exposure { get; set; } 
 public virtual ReserveType? ReserveType { get; set; } 
 public virtual ReserveStatus? Status { get; set; } 

    public static ClaimReserve FromRequest(ClaimReserveRequest request) {
        return new ClaimReserve {
            Id = request.Id,
            Amount = request.Amount,
            SetDate = request.SetDate,
            ReserveType = request.ReserveType,
            Status = request.Status,
        };
    }
}
