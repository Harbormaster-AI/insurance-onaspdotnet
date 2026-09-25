
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class ClaimPayment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ClaimpaymentId { get; set; } 
 public virtual string? PaymentNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
public virtual Claim? Claim { get; set; } 
public virtual Exposure? Exposure { get; set; } 
public virtual Beneficiary? Beneficiary { get; set; } 
public virtual ServiceProvider_? ServiceProvider_ { get; set; } 
public virtual Customer? Customer { get; set; } 
 public virtual PayeeType? PayeeType { get; set; } 
 public virtual PaymentMethod? Method { get; set; } 
 public virtual PaymentStatus? Status { get; set; } 

    public static ClaimPayment FromRequest(ClaimPaymentRequest request) {
        return new ClaimPayment {
            Id = request.Id,
            PaymentNumber = request.PaymentNumber,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            PayeeType = request.PayeeType,
            Method = request.Method,
            Status = request.Status,
        };
    }
}
