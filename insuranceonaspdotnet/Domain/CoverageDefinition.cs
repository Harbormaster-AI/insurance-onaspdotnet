
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Domain;

public class CoverageDefinition
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CoveragedefinitionId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual Money? DefaultLimit { get; set; } 
 public virtual Money? DefaultDeductible { get; set; } 
 public virtual bool? AsMandatory { get; set; } 
public virtual InsuranceProduct? Product { get; set; } 
 public virtual CoverageType? CoverageType { get; set; } 

    public static CoverageDefinition FromRequest(CoverageDefinitionRequest request) {
        return new CoverageDefinition {
            Id = request.Id,
            Name = request.Name,
            DefaultLimit = request.DefaultLimit,
            DefaultDeductible = request.DefaultDeductible,
            AsMandatory = request.AsMandatory,
            CoverageType = request.CoverageType,
        };
    }
}
