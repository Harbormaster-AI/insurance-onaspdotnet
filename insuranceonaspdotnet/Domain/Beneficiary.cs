
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Beneficiary
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? BeneficiaryId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Percentage? Share { get; set; } 
public virtual Policy? Policy { get; set; } 
public virtual Customer? Customer { get; set; } 
 public virtual RelationshipType? Relationship { get; set; } 

    public static Beneficiary FromRequest(BeneficiaryRequest request) {
        return new Beneficiary {
            Id = request.Id,
            Name = request.Name,
            Share = request.Share,
            Relationship = request.Relationship,
        };
    }
}
