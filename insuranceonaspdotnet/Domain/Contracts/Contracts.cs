using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class InsurerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? DomicileCountry { get; set; } 
 public virtual string? NaicNumber { get; set; } 
 public virtual string? Website { get; set; } 
}

public class InsurerResponse : InsurerRequest {
    public static InsurerResponse FromModel(Insurer model) {
        return new InsurerResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            DomicileCountry = model.DomicileCountry,
            NaicNumber = model.NaicNumber,
            Website = model.Website,
        };
    }
}

public class InsuranceProductRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ProductCode { get; set; } 
 public virtual LineOfBusiness? LineOfBusiness { get; set; } 
}

public class InsuranceProductResponse : InsuranceProductRequest {
    public static InsuranceProductResponse FromModel(InsuranceProduct model) {
        return new InsuranceProductResponse {
            Id = model.Id,
            Name = model.Name,
            ProductCode = model.ProductCode,
            LineOfBusiness = model.LineOfBusiness,
        };
    }
}

public class CoverageDefinitionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Money? DefaultLimit { get; set; } 
 public virtual Money? DefaultDeductible { get; set; } 
 public virtual bool? AsMandatory { get; set; } 
 public virtual CoverageType? CoverageType { get; set; } 
}

public class CoverageDefinitionResponse : CoverageDefinitionRequest {
    public static CoverageDefinitionResponse FromModel(CoverageDefinition model) {
        return new CoverageDefinitionResponse {
            Id = model.Id,
            Name = model.Name,
            DefaultLimit = model.DefaultLimit,
            DefaultDeductible = model.DefaultDeductible,
            AsMandatory = model.AsMandatory,
            CoverageType = model.CoverageType,
        };
    }
}

public class DistributorRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LicenseNumber { get; set; } 
 public virtual string? Region { get; set; } 
 public virtual DistributionChannelType? DistributorType { get; set; } 
}

public class DistributorResponse : DistributorRequest {
    public static DistributorResponse FromModel(Distributor model) {
        return new DistributorResponse {
            Id = model.Id,
            Name = model.Name,
            LicenseNumber = model.LicenseNumber,
            Region = model.Region,
            DistributorType = model.DistributorType,
        };
    }
}

public class AgentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? LicenseId { get; set; } 
 public virtual ProducerStatus? Status { get; set; } 
}

public class AgentResponse : AgentRequest {
    public static AgentResponse FromModel(Agent model) {
        return new AgentResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            LicenseId = model.LicenseId,
            Status = model.Status,
        };
    }
}

public class CustomerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? OrganizationName { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual DateOnly? DateOfBirth { get; set; } 
 public virtual Address? PrimaryAddress { get; set; } 
 public virtual CustomerType? CustomerType { get; set; } 
}

public class CustomerResponse : CustomerRequest {
    public static CustomerResponse FromModel(Customer model) {
        return new CustomerResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            OrganizationName = model.OrganizationName,
            TaxId = model.TaxId,
            DateOfBirth = model.DateOfBirth,
            PrimaryAddress = model.PrimaryAddress,
            CustomerType = model.CustomerType,
        };
    }
}

public class ApplicationRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ApplicationNumber { get; set; } 
 public virtual DateOnly? SubmissionDate { get; set; } 
 public virtual ApplicationStatus? Status { get; set; } 
}

public class ApplicationResponse : ApplicationRequest {
    public static ApplicationResponse FromModel(Application model) {
        return new ApplicationResponse {
            Id = model.Id,
            ApplicationNumber = model.ApplicationNumber,
            SubmissionDate = model.SubmissionDate,
            Status = model.Status,
        };
    }
}

public class QuoteRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? QuoteNumber { get; set; } 
 public virtual Money? TotalPremium { get; set; } 
 public virtual DateOnly? RatingDate { get; set; } 
 public virtual bool? AsBound { get; set; } 
}

public class QuoteResponse : QuoteRequest {
    public static QuoteResponse FromModel(Quote model) {
        return new QuoteResponse {
            Id = model.Id,
            QuoteNumber = model.QuoteNumber,
            TotalPremium = model.TotalPremium,
            RatingDate = model.RatingDate,
            AsBound = model.AsBound,
        };
    }
}

public class UnderwritingDecisionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Notes { get; set; } 
 public virtual DateOnly? DecisionDate { get; set; } 
 public virtual UnderwritingDecisionType? Decision { get; set; } 
}

public class UnderwritingDecisionResponse : UnderwritingDecisionRequest {
    public static UnderwritingDecisionResponse FromModel(UnderwritingDecision model) {
        return new UnderwritingDecisionResponse {
            Id = model.Id,
            Notes = model.Notes,
            DecisionDate = model.DecisionDate,
            Decision = model.Decision,
        };
    }
}

public class UnderwriterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? EmployeeId { get; set; } 
 public virtual Money? AuthorityLimit { get; set; } 
}

public class UnderwriterResponse : UnderwriterRequest {
    public static UnderwriterResponse FromModel(Underwriter model) {
        return new UnderwriterResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            EmployeeId = model.EmployeeId,
            AuthorityLimit = model.AuthorityLimit,
        };
    }
}

public class PolicyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual PolicyNumber? PolicyNumber { get; set; } 
 public virtual DateRange? EffectivePeriod { get; set; } 
 public virtual Money? TotalPremium { get; set; } 
 public virtual PolicyStatus? Status { get; set; } 
 public virtual PaymentPlanType? PaymentPlan { get; set; } 
}

public class PolicyResponse : PolicyRequest {
    public static PolicyResponse FromModel(Policy model) {
        return new PolicyResponse {
            Id = model.Id,
            PolicyNumber = model.PolicyNumber,
            EffectivePeriod = model.EffectivePeriod,
            TotalPremium = model.TotalPremium,
            Status = model.Status,
            PaymentPlan = model.PaymentPlan,
        };
    }
}

public class EndorsementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? EndorsementNumber { get; set; } 
 public virtual DateOnly? EffectiveDate { get; set; } 
 public virtual string? Description { get; set; } 
}

public class EndorsementResponse : EndorsementRequest {
    public static EndorsementResponse FromModel(Endorsement model) {
        return new EndorsementResponse {
            Id = model.Id,
            EndorsementNumber = model.EndorsementNumber,
            EffectiveDate = model.EffectiveDate,
            Description = model.Description,
        };
    }
}

public class PolicyCoverageRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? Limit { get; set; } 
 public virtual Money? Deductible { get; set; } 
 public virtual Money? Premium { get; set; } 
 public virtual CoverageType? CoverageType { get; set; } 
}

public class PolicyCoverageResponse : PolicyCoverageRequest {
    public static PolicyCoverageResponse FromModel(PolicyCoverage model) {
        return new PolicyCoverageResponse {
            Id = model.Id,
            Limit = model.Limit,
            Deductible = model.Deductible,
            Premium = model.Premium,
            CoverageType = model.CoverageType,
        };
    }
}

public class InsuredObjectRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Description { get; set; } 
 public virtual string? SerialOrId { get; set; } 
 public virtual Address? PrimaryAddress { get; set; } 
 public virtual InsuredObjectType? ObjectType { get; set; } 
}

public class InsuredObjectResponse : InsuredObjectRequest {
    public static InsuredObjectResponse FromModel(InsuredObject model) {
        return new InsuredObjectResponse {
            Id = model.Id,
            Description = model.Description,
            SerialOrId = model.SerialOrId,
            PrimaryAddress = model.PrimaryAddress,
            ObjectType = model.ObjectType,
        };
    }
}

public class BeneficiaryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Percentage? Share { get; set; } 
 public virtual RelationshipType? Relationship { get; set; } 
}

public class BeneficiaryResponse : BeneficiaryRequest {
    public static BeneficiaryResponse FromModel(Beneficiary model) {
        return new BeneficiaryResponse {
            Id = model.Id,
            Name = model.Name,
            Share = model.Share,
            Relationship = model.Relationship,
        };
    }
}

public class BillingAccountRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AccountNumber { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual BillingStatus? Status { get; set; } 
}

public class BillingAccountResponse : BillingAccountRequest {
    public static BillingAccountResponse FromModel(BillingAccount model) {
        return new BillingAccountResponse {
            Id = model.Id,
            AccountNumber = model.AccountNumber,
            Balance = model.Balance,
            Status = model.Status,
        };
    }
}

public class InvoiceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? InvoiceNumber { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? TotalDue { get; set; } 
 public virtual InvoiceStatus? Status { get; set; } 
}

public class InvoiceResponse : InvoiceRequest {
    public static InvoiceResponse FromModel(Invoice model) {
        return new InvoiceResponse {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            DueDate = model.DueDate,
            TotalDue = model.TotalDue,
            Status = model.Status,
        };
    }
}

public class PaymentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PaymentReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
 public virtual PaymentMethod? Method { get; set; } 
 public virtual PaymentStatus? Status { get; set; } 
}

public class PaymentResponse : PaymentRequest {
    public static PaymentResponse FromModel(Payment model) {
        return new PaymentResponse {
            Id = model.Id,
            PaymentReference = model.PaymentReference,
            Amount = model.Amount,
            PaymentDate = model.PaymentDate,
            Method = model.Method,
            Status = model.Status,
        };
    }
}

public class ClaimRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual ClaimNumber? ClaimNumber { get; set; } 
 public virtual DateOnly? NoticeDate { get; set; } 
 public virtual DateOnly? LossDate { get; set; } 
 public virtual string? ReportedBy { get; set; } 
 public virtual ClaimStatus? Status { get; set; } 
 public virtual CauseOfLoss? LossCause { get; set; } 
}

public class ClaimResponse : ClaimRequest {
    public static ClaimResponse FromModel(Claim model) {
        return new ClaimResponse {
            Id = model.Id,
            ClaimNumber = model.ClaimNumber,
            NoticeDate = model.NoticeDate,
            LossDate = model.LossDate,
            ReportedBy = model.ReportedBy,
            Status = model.Status,
            LossCause = model.LossCause,
        };
    }
}

public class IncidentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Address? Location { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual PerilType? IncidentType { get; set; } 
}

public class IncidentResponse : IncidentRequest {
    public static IncidentResponse FromModel(Incident model) {
        return new IncidentResponse {
            Id = model.Id,
            Location = model.Location,
            Description = model.Description,
            IncidentType = model.IncidentType,
        };
    }
}

public class ExposureRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual ExposureType? ExposureType { get; set; } 
 public virtual ExposureStatus? Status { get; set; } 
}

public class ExposureResponse : ExposureRequest {
    public static ExposureResponse FromModel(Exposure model) {
        return new ExposureResponse {
            Id = model.Id,
            ExposureType = model.ExposureType,
            Status = model.Status,
        };
    }
}

public class AdjusterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? LicenseNumber { get; set; } 
 public virtual AdjusterType? AdjusterType { get; set; } 
}

public class AdjusterResponse : AdjusterRequest {
    public static AdjusterResponse FromModel(Adjuster model) {
        return new AdjusterResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            LicenseNumber = model.LicenseNumber,
            AdjusterType = model.AdjusterType,
        };
    }
}

public class ClaimReserveRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? SetDate { get; set; } 
 public virtual ReserveType? ReserveType { get; set; } 
 public virtual ReserveStatus? Status { get; set; } 
}

public class ClaimReserveResponse : ClaimReserveRequest {
    public static ClaimReserveResponse FromModel(ClaimReserve model) {
        return new ClaimReserveResponse {
            Id = model.Id,
            Amount = model.Amount,
            SetDate = model.SetDate,
            ReserveType = model.ReserveType,
            Status = model.Status,
        };
    }
}

public class ClaimPaymentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PaymentNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? PaymentDate { get; set; } 
 public virtual PayeeType? PayeeType { get; set; } 
 public virtual PaymentMethod? Method { get; set; } 
 public virtual PaymentStatus? Status { get; set; } 
}

public class ClaimPaymentResponse : ClaimPaymentRequest {
    public static ClaimPaymentResponse FromModel(ClaimPayment model) {
        return new ClaimPaymentResponse {
            Id = model.Id,
            PaymentNumber = model.PaymentNumber,
            Amount = model.Amount,
            PaymentDate = model.PaymentDate,
            PayeeType = model.PayeeType,
            Method = model.Method,
            Status = model.Status,
        };
    }
}

public class ServiceProvider_Request {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual ServiceProviderType? ProviderType { get; set; } 
 public virtual NetworkStatus? NetworkStatus { get; set; } 
}

public class ServiceProvider_Response : ServiceProvider_Request {
    public static ServiceProvider_Response FromModel(ServiceProvider_ model) {
        return new ServiceProvider_Response {
            Id = model.Id,
            Name = model.Name,
            TaxId = model.TaxId,
            ProviderType = model.ProviderType,
            NetworkStatus = model.NetworkStatus,
        };
    }
}

public class ReinsuranceAgreementRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? AgreementNumber { get; set; } 
 public virtual DateRange? EffectivePeriod { get; set; } 
 public virtual Money? Retention { get; set; } 
 public virtual Money? Limit { get; set; } 
 public virtual Percentage? CessionPercentage { get; set; } 
 public virtual ReinsuranceType? ReinsuranceType { get; set; } 
 public virtual TreatyType? TreatyType { get; set; } 
}

public class ReinsuranceAgreementResponse : ReinsuranceAgreementRequest {
    public static ReinsuranceAgreementResponse FromModel(ReinsuranceAgreement model) {
        return new ReinsuranceAgreementResponse {
            Id = model.Id,
            AgreementNumber = model.AgreementNumber,
            EffectivePeriod = model.EffectivePeriod,
            Retention = model.Retention,
            Limit = model.Limit,
            CessionPercentage = model.CessionPercentage,
            ReinsuranceType = model.ReinsuranceType,
            TreatyType = model.TreatyType,
        };
    }
}

public class SubrogationRecoveryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RecoveryReference { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? RecoveryDate { get; set; } 
 public virtual SubrogationStatus? Status { get; set; } 
}

public class SubrogationRecoveryResponse : SubrogationRecoveryRequest {
    public static SubrogationRecoveryResponse FromModel(SubrogationRecovery model) {
        return new SubrogationRecoveryResponse {
            Id = model.Id,
            RecoveryReference = model.RecoveryReference,
            Amount = model.Amount,
            RecoveryDate = model.RecoveryDate,
            Status = model.Status,
        };
    }
}

public class ThirdPartyRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? TaxId { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual ThirdPartyType? PartyType { get; set; } 
}

public class ThirdPartyResponse : ThirdPartyRequest {
    public static ThirdPartyResponse FromModel(ThirdParty model) {
        return new ThirdPartyResponse {
            Id = model.Id,
            Name = model.Name,
            TaxId = model.TaxId,
            Address = model.Address,
            PartyType = model.PartyType,
        };
    }
}

public class DocumentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FileName { get; set; } 
 public virtual DateOnly? UploadedDate { get; set; } 
 public virtual DocumentType? DocumentType { get; set; } 
}

public class DocumentResponse : DocumentRequest {
    public static DocumentResponse FromModel(Document model) {
        return new DocumentResponse {
            Id = model.Id,
            FileName = model.FileName,
            UploadedDate = model.UploadedDate,
            DocumentType = model.DocumentType,
        };
    }
}

