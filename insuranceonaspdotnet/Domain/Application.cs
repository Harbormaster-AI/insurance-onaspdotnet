
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Application
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ApplicationId { get; set; } 
 public virtual string? ApplicationNumber { get; set; } 
 public virtual DateOnly? SubmissionDate { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual InsuranceProduct? Product { get; set; } 
public virtual Distributor? Distributor { get; set; } 
public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
public virtual Quote? SelectedQuote { get; set; } 
 public virtual ApplicationStatus? Status { get; set; } 

    public static Application FromRequest(ApplicationRequest request) {
        return new Application {
            Id = request.Id,
            ApplicationNumber = request.ApplicationNumber,
            SubmissionDate = request.SubmissionDate,
            Status = request.Status,
        };
    }
}
