
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Agent
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? AgentId { get; set; } 
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? LicenseId { get; set; } 
public virtual Distributor? Distributor { get; set; } 
public virtual ICollection<Policy> Policies { get; set; } = new List<Policy>();
public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
 public virtual ProducerStatus? Status { get; set; } 

    public static Agent FromRequest(AgentRequest request) {
        return new Agent {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseId = request.LicenseId,
            Status = request.Status,
        };
    }
}
