
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class ServiceProvider_
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? Serviceprovider_Id { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? TaxId { get; set; } 
public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
 public virtual ServiceProviderType? ProviderType { get; set; } 
 public virtual NetworkStatus? NetworkStatus { get; set; } 

    public static ServiceProvider_ FromRequest(ServiceProvider_Request request) {
        return new ServiceProvider_ {
            Id = request.Id,
            Name = request.Name,
            TaxId = request.TaxId,
            ProviderType = request.ProviderType,
            NetworkStatus = request.NetworkStatus,
        };
    }
}
