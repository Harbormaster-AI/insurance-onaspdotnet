
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InvoiceId { get; set; } 
 public virtual string? InvoiceNumber { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? TotalDue { get; set; } 
public virtual BillingAccount? BillingAccount { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
 public virtual InvoiceStatus? Status { get; set; } 

    public static Invoice FromRequest(InvoiceRequest request) {
        return new Invoice {
            Id = request.Id,
            InvoiceNumber = request.InvoiceNumber,
            DueDate = request.DueDate,
            TotalDue = request.TotalDue,
            Status = request.Status,
        };
    }
}
