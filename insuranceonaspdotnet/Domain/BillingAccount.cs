
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class BillingAccount
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BillingaccountId { get; set; } 
 public virtual string? AccountNumber { get; set; } 
 public virtual Money? Balance { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
 public virtual BillingStatus? Status { get; set; } 

    public static BillingAccount FromRequest(BillingAccountRequest request) {
        return new BillingAccount {
            Id = request.Id,
            AccountNumber = request.AccountNumber,
            Balance = request.Balance,
            Status = request.Status,
        };
    }
}
