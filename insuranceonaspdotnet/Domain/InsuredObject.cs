
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class InsuredObject
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InsuredobjectId { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual string? SerialOrId { get; set; } 
 public virtual Address? PrimaryAddress { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual ICollection<PolicyCoverage> Coverages { get; set; } = new List<PolicyCoverage>();
 public virtual InsuredObjectType? ObjectType { get; set; } 

    public static InsuredObject FromRequest(InsuredObjectRequest request) {
        return new InsuredObject {
            Id = request.Id,
            Description = request.Description,
            SerialOrId = request.SerialOrId,
            PrimaryAddress = request.PrimaryAddress,
            ObjectType = request.ObjectType,
        };
    }
}
