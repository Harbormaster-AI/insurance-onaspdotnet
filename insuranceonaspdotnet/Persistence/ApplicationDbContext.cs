using Microsoft.EntityFrameworkCore;

using insuranceonaspdotnet.Domain;

namespace insuranceonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Insurer> Insurers => Set<Insurer>();
public DbSet<InsuranceProduct> InsuranceProducts => Set<InsuranceProduct>();
public DbSet<CoverageDefinition> CoverageDefinitions => Set<CoverageDefinition>();
public DbSet<Distributor> Distributors => Set<Distributor>();
public DbSet<Agent> Agents => Set<Agent>();
public DbSet<Customer> Customers => Set<Customer>();
public DbSet<Application> Applications => Set<Application>();
public DbSet<Quote> Quotes => Set<Quote>();
public DbSet<UnderwritingDecision> UnderwritingDecisions => Set<UnderwritingDecision>();
public DbSet<Underwriter> Underwriters => Set<Underwriter>();
public DbSet<Policy> Policys => Set<Policy>();
public DbSet<Endorsement> Endorsements => Set<Endorsement>();
public DbSet<PolicyCoverage> PolicyCoverages => Set<PolicyCoverage>();
public DbSet<InsuredObject> InsuredObjects => Set<InsuredObject>();
public DbSet<Beneficiary> Beneficiarys => Set<Beneficiary>();
public DbSet<BillingAccount> BillingAccounts => Set<BillingAccount>();
public DbSet<Invoice> Invoices => Set<Invoice>();
public DbSet<Payment> Payments => Set<Payment>();
public DbSet<Claim> Claims => Set<Claim>();
public DbSet<Incident> Incidents => Set<Incident>();
public DbSet<Exposure> Exposures => Set<Exposure>();
public DbSet<Adjuster> Adjusters => Set<Adjuster>();
public DbSet<ClaimReserve> ClaimReserves => Set<ClaimReserve>();
public DbSet<ClaimPayment> ClaimPayments => Set<ClaimPayment>();
public DbSet<ServiceProvider> ServiceProviders => Set<ServiceProvider>();
public DbSet<ReinsuranceAgreement> ReinsuranceAgreements => Set<ReinsuranceAgreement>();
public DbSet<SubrogationRecovery> SubrogationRecoverys => Set<SubrogationRecovery>();
public DbSet<ThirdParty> ThirdPartys => Set<ThirdParty>();
public DbSet<Document> Documents => Set<Document>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Insurer has one or more Products of type InsuranceProduct
        modelBuilder.Entity<InsuranceProduct>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("ProductsId");

        // Insurer has one or more DistributionPartners of type Distributor
        modelBuilder.Entity<Distributor>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.DistributionPartners)
            .HasForeignKey("DistributionPartnersId");

        // Insurer has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // Insurer has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // Insurer has one or more ReinsuranceAgreements of type ReinsuranceAgreement
        modelBuilder.Entity<ReinsuranceAgreement>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.ReinsuranceAgreements)
            .HasForeignKey("ReinsuranceAgreementsId");

        // InsuranceProduct has one Insurer of type Insurer
        modelBuilder.Entity<InsuranceProduct>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("InsurerId");


        // InsuranceProduct has one or more CoverageDefinitions of type CoverageDefinition
        modelBuilder.Entity<CoverageDefinition>()
            .HasOne<InsuranceProduct>()
            .WithMany(parent => parent.CoverageDefinitions)
            .HasForeignKey("CoverageDefinitionsId");

        // CoverageDefinition has one Product of type InsuranceProduct
        modelBuilder.Entity<CoverageDefinition>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId");



        // Distributor has one or more Insurers of type Insurer
        modelBuilder.Entity<Insurer>()
            .HasOne<Distributor>()
            .WithMany(parent => parent.Insurers)
            .HasForeignKey("InsurersId");

        // Distributor has one or more Agents of type Agent
        modelBuilder.Entity<Agent>()
            .HasOne<Distributor>()
            .WithMany(parent => parent.Agents)
            .HasForeignKey("AgentsId");

        // Distributor has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Distributor>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // Agent has one Distributor of type Distributor
        modelBuilder.Entity<Agent>()
            .HasOne(x => x.Distributor)
            .WithMany()
            .HasForeignKey("DistributorId");


        // Agent has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Agent>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // Agent has one or more Customers of type Customer
        modelBuilder.Entity<Customer>()
            .HasOne<Agent>()
            .WithMany(parent => parent.Customers)
            .HasForeignKey("CustomersId");


        // Customer has one or more Applications of type Application
        modelBuilder.Entity<Application>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Applications)
            .HasForeignKey("ApplicationsId");

        // Customer has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // Customer has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // Customer has one or more Agents of type Agent
        modelBuilder.Entity<Agent>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Agents)
            .HasForeignKey("AgentsId");

        // Customer has one or more Beneficiaries of type Beneficiary
        modelBuilder.Entity<Beneficiary>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Beneficiaries)
            .HasForeignKey("BeneficiariesId");

        // Application has one Customer of type Customer
        modelBuilder.Entity<Application>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Application has one Product of type InsuranceProduct
        modelBuilder.Entity<Application>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId");

        // Application has one Distributor of type Distributor
        modelBuilder.Entity<Application>()
            .HasOne(x => x.Distributor)
            .WithMany()
            .HasForeignKey("DistributorId");

        // Application has one SelectedQuote of type Quote
        modelBuilder.Entity<Application>()
            .HasOne(x => x.SelectedQuote)
            .WithMany()
            .HasForeignKey("SelectedQuoteId");


        // Application has one or more Quotes of type Quote
        modelBuilder.Entity<Quote>()
            .HasOne<Application>()
            .WithMany(parent => parent.Quotes)
            .HasForeignKey("QuotesId");

        // Quote has one Application of type Application
        modelBuilder.Entity<Quote>()
            .HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey("ApplicationId");

        // Quote has one Policy of type Policy
        modelBuilder.Entity<Quote>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // Quote has one or more UnderwritingDecisions of type UnderwritingDecision
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne<Quote>()
            .WithMany(parent => parent.UnderwritingDecisions)
            .HasForeignKey("UnderwritingDecisionsId");

        // UnderwritingDecision has one Quote of type Quote
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne(x => x.Quote)
            .WithMany()
            .HasForeignKey("QuoteId");

        // UnderwritingDecision has one Underwriter of type Underwriter
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne(x => x.Underwriter)
            .WithMany()
            .HasForeignKey("UnderwriterId");


        // Underwriter has one Insurer of type Insurer
        modelBuilder.Entity<Underwriter>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("InsurerId");


        // Underwriter has one or more Decisions of type UnderwritingDecision
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne<Underwriter>()
            .WithMany(parent => parent.Decisions)
            .HasForeignKey("DecisionsId");

        // Policy has one Insurer of type Insurer
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("InsurerId");

        // Policy has one Customer of type Customer
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Policy has one Product of type InsuranceProduct
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId");

        // Policy has one Agent of type Agent
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Agent)
            .WithMany()
            .HasForeignKey("AgentId");

        // Policy has one BillingAccount of type BillingAccount
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.BillingAccount)
            .WithMany()
            .HasForeignKey("BillingAccountId");


        // Policy has one or more Coverages of type PolicyCoverage
        modelBuilder.Entity<PolicyCoverage>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("CoveragesId");

        // Policy has one or more InsuredObjects of type InsuredObject
        modelBuilder.Entity<InsuredObject>()
            .HasOne<Policy>()
            .WithMany(parent => parent.InsuredObjects)
            .HasForeignKey("InsuredObjectsId");

        // Policy has one or more Endorsements of type Endorsement
        modelBuilder.Entity<Endorsement>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Endorsements)
            .HasForeignKey("EndorsementsId");

        // Policy has one or more Beneficiaries of type Beneficiary
        modelBuilder.Entity<Beneficiary>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Beneficiaries)
            .HasForeignKey("BeneficiariesId");

        // Policy has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // Policy has one or more ReinsuranceAgreements of type ReinsuranceAgreement
        modelBuilder.Entity<ReinsuranceAgreement>()
            .HasOne<Policy>()
            .WithMany(parent => parent.ReinsuranceAgreements)
            .HasForeignKey("ReinsuranceAgreementsId");

        // Endorsement has one Policy of type Policy
        modelBuilder.Entity<Endorsement>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // PolicyCoverage has one Policy of type Policy
        modelBuilder.Entity<PolicyCoverage>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // PolicyCoverage has one or more InsuredObjects of type InsuredObject
        modelBuilder.Entity<InsuredObject>()
            .HasOne<PolicyCoverage>()
            .WithMany(parent => parent.InsuredObjects)
            .HasForeignKey("InsuredObjectsId");

        // InsuredObject has one Policy of type Policy
        modelBuilder.Entity<InsuredObject>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // InsuredObject has one or more Coverages of type PolicyCoverage
        modelBuilder.Entity<PolicyCoverage>()
            .HasOne<InsuredObject>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("CoveragesId");

        // Beneficiary has one Policy of type Policy
        modelBuilder.Entity<Beneficiary>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");

        // Beneficiary has one Customer of type Customer
        modelBuilder.Entity<Beneficiary>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");


        // BillingAccount has one Customer of type Customer
        modelBuilder.Entity<BillingAccount>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");


        // BillingAccount has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<BillingAccount>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // BillingAccount has one or more Invoices of type Invoice
        modelBuilder.Entity<Invoice>()
            .HasOne<BillingAccount>()
            .WithMany(parent => parent.Invoices)
            .HasForeignKey("InvoicesId");

        // BillingAccount has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<BillingAccount>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");

        // Invoice has one BillingAccount of type BillingAccount
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.BillingAccount)
            .WithMany()
            .HasForeignKey("BillingAccountId");

        // Invoice has one Policy of type Policy
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // Invoice has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Invoice>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");

        // Payment has one Invoice of type Invoice
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey("InvoiceId");

        // Payment has one BillingAccount of type BillingAccount
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.BillingAccount)
            .WithMany()
            .HasForeignKey("BillingAccountId");

        // Payment has one Policy of type Policy
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");


        // Claim has one Policy of type Policy
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");

        // Claim has one Customer of type Customer
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Claim has one Adjuster of type Adjuster
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Adjuster)
            .WithMany()
            .HasForeignKey("AdjusterId");

        // Claim has one Incident of type Incident
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Incident)
            .WithMany()
            .HasForeignKey("IncidentId");


        // Claim has one or more Exposures of type Exposure
        modelBuilder.Entity<Exposure>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Exposures)
            .HasForeignKey("ExposuresId");

        // Claim has one or more Reserves of type ClaimReserve
        modelBuilder.Entity<ClaimReserve>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Reserves)
            .HasForeignKey("ReservesId");

        // Claim has one or more ClaimPayments of type ClaimPayment
        modelBuilder.Entity<ClaimPayment>()
            .HasOne<Claim>()
            .WithMany(parent => parent.ClaimPayments)
            .HasForeignKey("ClaimPaymentsId");

        // Claim has one or more ServiceProviders of type ServiceProvider
        modelBuilder.Entity<ServiceProvider>()
            .HasOne<Claim>()
            .WithMany(parent => parent.ServiceProviders)
            .HasForeignKey("ServiceProvidersId");

        // Claim has one or more Subrogations of type SubrogationRecovery
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Subrogations)
            .HasForeignKey("SubrogationsId");

        // Incident has one Claim of type Claim
        modelBuilder.Entity<Incident>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");


        // Incident has one or more InsuredObjects of type InsuredObject
        modelBuilder.Entity<InsuredObject>()
            .HasOne<Incident>()
            .WithMany(parent => parent.InsuredObjects)
            .HasForeignKey("InsuredObjectsId");

        // Exposure has one Claim of type Claim
        modelBuilder.Entity<Exposure>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");

        // Exposure has one PolicyCoverage of type PolicyCoverage
        modelBuilder.Entity<Exposure>()
            .HasOne(x => x.PolicyCoverage)
            .WithMany()
            .HasForeignKey("PolicyCoverageId");

        // Exposure has one InsuredObject of type InsuredObject
        modelBuilder.Entity<Exposure>()
            .HasOne(x => x.InsuredObject)
            .WithMany()
            .HasForeignKey("InsuredObjectId");


        // Exposure has one or more Reserves of type ClaimReserve
        modelBuilder.Entity<ClaimReserve>()
            .HasOne<Exposure>()
            .WithMany(parent => parent.Reserves)
            .HasForeignKey("ReservesId");

        // Exposure has one or more Payments of type ClaimPayment
        modelBuilder.Entity<ClaimPayment>()
            .HasOne<Exposure>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");


        // Adjuster has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Adjuster>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // Adjuster has one or more ServiceProviders of type ServiceProvider
        modelBuilder.Entity<ServiceProvider>()
            .HasOne<Adjuster>()
            .WithMany(parent => parent.ServiceProviders)
            .HasForeignKey("ServiceProvidersId");

        // ClaimReserve has one Claim of type Claim
        modelBuilder.Entity<ClaimReserve>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");

        // ClaimReserve has one Exposure of type Exposure
        modelBuilder.Entity<ClaimReserve>()
            .HasOne(x => x.Exposure)
            .WithMany()
            .HasForeignKey("ExposureId");


        // ClaimPayment has one Claim of type Claim
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");

        // ClaimPayment has one Exposure of type Exposure
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Exposure)
            .WithMany()
            .HasForeignKey("ExposureId");

        // ClaimPayment has one Beneficiary of type Beneficiary
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Beneficiary)
            .WithMany()
            .HasForeignKey("BeneficiaryId");

        // ClaimPayment has one ServiceProvider of type ServiceProvider
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.ServiceProvider)
            .WithMany()
            .HasForeignKey("ServiceProviderId");

        // ClaimPayment has one Customer of type Customer
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");



        // ServiceProvider has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<ServiceProvider>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ClaimsId");

        // ReinsuranceAgreement has one Insurer of type Insurer
        modelBuilder.Entity<ReinsuranceAgreement>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("InsurerId");


        // ReinsuranceAgreement has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<ReinsuranceAgreement>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("PoliciesId");

        // SubrogationRecovery has one Claim of type Claim
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");

        // SubrogationRecovery has one Exposure of type Exposure
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne(x => x.Exposure)
            .WithMany()
            .HasForeignKey("ExposureId");

        // SubrogationRecovery has one Counterparty of type ThirdParty
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne(x => x.Counterparty)
            .WithMany()
            .HasForeignKey("CounterpartyId");



        // ThirdParty has one or more Subrogations of type SubrogationRecovery
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.Subrogations)
            .HasForeignKey("SubrogationsId");

        // Document has one Policy of type Policy
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("PolicyId");

        // Document has one Claim of type Claim
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("ClaimId");

        // Document has one Application of type Application
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey("ApplicationId");

        // Document has one Customer of type Customer
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");


    }
}
