using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class Endorsement
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? EndorsementId { get; set; } 
 public virtual string? EndorsementNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual string? Description { get; set; } 
public virtual Policy? Policy { get; set; } 

    public static Endorsement FromRequest(EndorsementRequest request) {
        return new Endorsement {
            Id = request.Id,
            EndorsementNumber = request.EndorsementNumber,
            EffectiveDate = request.EffectiveDate,
            Description = request.Description,
        };
    }
}
