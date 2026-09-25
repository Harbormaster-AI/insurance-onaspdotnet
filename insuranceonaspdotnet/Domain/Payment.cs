
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Payment
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? PaymentId { get; set; } 
 public virtual string? PaymentReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
public virtual Invoice? Invoice { get; set; } 
public virtual BillingAccount? BillingAccount { get; set; } 
public virtual Policy? Policy { get; set; } 
 public virtual PaymentMethod? Method { get; set; } 
 public virtual PaymentStatus? Status { get; set; } 

    public static Payment FromRequest(PaymentRequest request) {
        return new Payment {
            Id = request.Id,
            PaymentReference = request.PaymentReference,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            Method = request.Method,
            Status = request.Status,
        };
    }
}
