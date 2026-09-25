
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class ThirdParty
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ThirdpartyId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual Address? Address { get; set; } 
public virtual ICollection<SubrogationRecovery> Subrogations { get; set; } = new List<SubrogationRecovery>();
 public virtual ThirdPartyType? PartyType { get; set; } 

    public static ThirdParty FromRequest(ThirdPartyRequest request) {
        return new ThirdParty {
            Id = request.Id,
            Name = request.Name,
            TaxId = request.TaxId,
            Address = request.Address,
            PartyType = request.PartyType,
        };
    }
}
