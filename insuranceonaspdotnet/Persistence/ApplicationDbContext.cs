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
    public DbSet<ServiceProvider_> ServiceProvider_s => Set<ServiceProvider_>();
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
            .HasForeignKey("Insurer_Id");

        // Insurer has one or more DistributionPartners of type Distributor
        modelBuilder.Entity<Distributor>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.DistributionPartners)
            .HasForeignKey("Insurer_Id");

        // Insurer has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("Insurer_Id");

        // Insurer has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("Insurer_Id");

        // Insurer has one or more ReinsuranceAgreements of type ReinsuranceAgreement
        modelBuilder.Entity<ReinsuranceAgreement>()
            .HasOne<Insurer>()
            .WithMany(parent => parent.ReinsuranceAgreements)
            .HasForeignKey("Insurer_Id");

        // InsuranceProduct has one Insurer of type Insurer
        modelBuilder.Entity<InsuranceProduct>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("Insurer_Id");


        // InsuranceProduct has one or more CoverageDefinitions of type CoverageDefinition
        modelBuilder.Entity<CoverageDefinition>()
            .HasOne<InsuranceProduct>()
            .WithMany(parent => parent.CoverageDefinitions)
            .HasForeignKey("InsuranceProduct_Id");

        // CoverageDefinition has one Product of type InsuranceProduct
        modelBuilder.Entity<CoverageDefinition>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("Product_Id");



        // Distributor has one or more Insurers of type Insurer
        modelBuilder.Entity<Insurer>()
            .HasOne<Distributor>()
            .WithMany(parent => parent.Insurers)
            .HasForeignKey("Distributor_Id");

        // Distributor has one or more Agents of type Agent
        modelBuilder.Entity<Agent>()
            .HasOne<Distributor>()
            .WithMany(parent => parent.Agents)
            .HasForeignKey("Distributor_Id");

        // Distributor has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Distributor>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("Distributor_Id");

        // Agent has one Distributor of type Distributor
        modelBuilder.Entity<Agent>()
            .HasOne(x => x.Distributor)
            .WithMany()
            .HasForeignKey("Distributor_Id");


        // Agent has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Agent>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("Agent_Id");

        // Agent has one or more Customers of type Customer
        modelBuilder.Entity<Customer>()
            .HasOne<Agent>()
            .WithMany(parent => parent.Customers)
            .HasForeignKey("Agent_Id");


        // Customer has one or more Applications of type Application
        modelBuilder.Entity<Application>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Applications)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Agents of type Agent
        modelBuilder.Entity<Agent>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Agents)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Beneficiaries of type Beneficiary
        modelBuilder.Entity<Beneficiary>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Beneficiaries)
            .HasForeignKey("Customer_Id");

        // Application has one Customer of type Customer
        modelBuilder.Entity<Application>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Application has one Product of type InsuranceProduct
        modelBuilder.Entity<Application>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("Product_Id");

        // Application has one Distributor of type Distributor
        modelBuilder.Entity<Application>()
            .HasOne(x => x.Distributor)
            .WithMany()
            .HasForeignKey("Distributor_Id");

        // Application has one SelectedQuote of type Quote
        modelBuilder.Entity<Application>()
            .HasOne(x => x.SelectedQuote)
            .WithMany()
            .HasForeignKey("SelectedQuote_Id");


        // Application has one or more Quotes of type Quote
        modelBuilder.Entity<Quote>()
            .HasOne<Application>()
            .WithMany(parent => parent.Quotes)
            .HasForeignKey("Application_Id");

        // Quote has one Application of type Application
        modelBuilder.Entity<Quote>()
            .HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey("Application_Id");

        // Quote has one Policy of type Policy
        modelBuilder.Entity<Quote>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");


        // Quote has one or more UnderwritingDecisions of type UnderwritingDecision
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne<Quote>()
            .WithMany(parent => parent.UnderwritingDecisions)
            .HasForeignKey("Quote_Id");

        // UnderwritingDecision has one Quote of type Quote
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne(x => x.Quote)
            .WithMany()
            .HasForeignKey("Quote_Id");

        // UnderwritingDecision has one Underwriter of type Underwriter
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne(x => x.Underwriter)
            .WithMany()
            .HasForeignKey("Underwriter_Id");


        // Underwriter has one Insurer of type Insurer
        modelBuilder.Entity<Underwriter>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("Insurer_Id");


        // Underwriter has one or more Decisions of type UnderwritingDecision
        modelBuilder.Entity<UnderwritingDecision>()
            .HasOne<Underwriter>()
            .WithMany(parent => parent.Decisions)
            .HasForeignKey("Underwriter_Id");

        // Policy has one Insurer of type Insurer
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("Insurer_Id");

        // Policy has one Customer of type Customer
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Policy has one Product of type InsuranceProduct
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("Product_Id");

        // Policy has one Agent of type Agent
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.Agent)
            .WithMany()
            .HasForeignKey("Agent_Id");

        // Policy has one BillingAccount of type BillingAccount
        modelBuilder.Entity<Policy>()
            .HasOne(x => x.BillingAccount)
            .WithMany()
            .HasForeignKey("BillingAccount_Id");


        // Policy has one or more Coverages of type PolicyCoverage
        modelBuilder.Entity<PolicyCoverage>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("Policy_Id");

        // Policy has one or more InsuredObjects of type InsuredObject
        modelBuilder.Entity<InsuredObject>()
            .HasOne<Policy>()
            .WithMany(parent => parent.InsuredObjects)
            .HasForeignKey("Policy_Id");

        // Policy has one or more Endorsements of type Endorsement
        modelBuilder.Entity<Endorsement>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Endorsements)
            .HasForeignKey("Policy_Id");

        // Policy has one or more Beneficiaries of type Beneficiary
        modelBuilder.Entity<Beneficiary>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Beneficiaries)
            .HasForeignKey("Policy_Id");

        // Policy has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Policy>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("Policy_Id");

        // Policy has one or more ReinsuranceAgreements of type ReinsuranceAgreement
        modelBuilder.Entity<ReinsuranceAgreement>()
            .HasOne<Policy>()
            .WithMany(parent => parent.ReinsuranceAgreements)
            .HasForeignKey("Policy_Id");

        // Endorsement has one Policy of type Policy
        modelBuilder.Entity<Endorsement>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");


        // PolicyCoverage has one Policy of type Policy
        modelBuilder.Entity<PolicyCoverage>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");


        // PolicyCoverage has one or more InsuredObjects of type InsuredObject
        modelBuilder.Entity<InsuredObject>()
            .HasOne<PolicyCoverage>()
            .WithMany(parent => parent.InsuredObjects)
            .HasForeignKey("PolicyCoverage_Id");

        // InsuredObject has one Policy of type Policy
        modelBuilder.Entity<InsuredObject>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");


        // InsuredObject has one or more Coverages of type PolicyCoverage
        modelBuilder.Entity<PolicyCoverage>()
            .HasOne<InsuredObject>()
            .WithMany(parent => parent.Coverages)
            .HasForeignKey("InsuredObject_Id");

        // Beneficiary has one Policy of type Policy
        modelBuilder.Entity<Beneficiary>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");

        // Beneficiary has one Customer of type Customer
        modelBuilder.Entity<Beneficiary>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // BillingAccount has one Customer of type Customer
        modelBuilder.Entity<BillingAccount>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // BillingAccount has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<BillingAccount>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("BillingAccount_Id");

        // BillingAccount has one or more Invoices of type Invoice
        modelBuilder.Entity<Invoice>()
            .HasOne<BillingAccount>()
            .WithMany(parent => parent.Invoices)
            .HasForeignKey("BillingAccount_Id");

        // BillingAccount has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<BillingAccount>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("BillingAccount_Id");

        // Invoice has one BillingAccount of type BillingAccount
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.BillingAccount)
            .WithMany()
            .HasForeignKey("BillingAccount_Id");

        // Invoice has one Policy of type Policy
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");


        // Invoice has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Invoice>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("Invoice_Id");

        // Payment has one Invoice of type Invoice
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey("Invoice_Id");

        // Payment has one BillingAccount of type BillingAccount
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.BillingAccount)
            .WithMany()
            .HasForeignKey("BillingAccount_Id");

        // Payment has one Policy of type Policy
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");


        // Claim has one Policy of type Policy
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");

        // Claim has one Customer of type Customer
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Claim has one Adjuster of type Adjuster
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Adjuster)
            .WithMany()
            .HasForeignKey("Adjuster_Id");

        // Claim has one Incident of type Incident
        modelBuilder.Entity<Claim>()
            .HasOne(x => x.Incident)
            .WithMany()
            .HasForeignKey("Incident_Id");


        // Claim has one or more Exposures of type Exposure
        modelBuilder.Entity<Exposure>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Exposures)
            .HasForeignKey("Claim_Id");

        // Claim has one or more Reserves of type ClaimReserve
        modelBuilder.Entity<ClaimReserve>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Reserves)
            .HasForeignKey("Claim_Id");

        // Claim has one or more ClaimPayments of type ClaimPayment
        modelBuilder.Entity<ClaimPayment>()
            .HasOne<Claim>()
            .WithMany(parent => parent.ClaimPayments)
            .HasForeignKey("Claim_Id");

        // Claim has one or more ServiceProviders of type ServiceProvider_
        modelBuilder.Entity<ServiceProvider_>()
            .HasOne<Claim>()
            .WithMany(parent => parent.ServiceProviders)
            .HasForeignKey("Claim_Id");

        // Claim has one or more Subrogations of type SubrogationRecovery
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne<Claim>()
            .WithMany(parent => parent.Subrogations)
            .HasForeignKey("Claim_Id");

        // Incident has one Claim of type Claim
        modelBuilder.Entity<Incident>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");


        // Incident has one or more InsuredObjects of type InsuredObject
        modelBuilder.Entity<InsuredObject>()
            .HasOne<Incident>()
            .WithMany(parent => parent.InsuredObjects)
            .HasForeignKey("Incident_Id");

        // Exposure has one Claim of type Claim
        modelBuilder.Entity<Exposure>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");

        // Exposure has one PolicyCoverage of type PolicyCoverage
        modelBuilder.Entity<Exposure>()
            .HasOne(x => x.PolicyCoverage)
            .WithMany()
            .HasForeignKey("PolicyCoverage_Id");

        // Exposure has one InsuredObject of type InsuredObject
        modelBuilder.Entity<Exposure>()
            .HasOne(x => x.InsuredObject)
            .WithMany()
            .HasForeignKey("InsuredObject_Id");


        // Exposure has one or more Reserves of type ClaimReserve
        modelBuilder.Entity<ClaimReserve>()
            .HasOne<Exposure>()
            .WithMany(parent => parent.Reserves)
            .HasForeignKey("Exposure_Id");

        // Exposure has one or more Payments of type ClaimPayment
        modelBuilder.Entity<ClaimPayment>()
            .HasOne<Exposure>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("Exposure_Id");


        // Adjuster has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<Adjuster>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("Adjuster_Id");

        // Adjuster has one or more ServiceProviders of type ServiceProvider_
        modelBuilder.Entity<ServiceProvider_>()
            .HasOne<Adjuster>()
            .WithMany(parent => parent.ServiceProviders)
            .HasForeignKey("Adjuster_Id");

        // ClaimReserve has one Claim of type Claim
        modelBuilder.Entity<ClaimReserve>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");

        // ClaimReserve has one Exposure of type Exposure
        modelBuilder.Entity<ClaimReserve>()
            .HasOne(x => x.Exposure)
            .WithMany()
            .HasForeignKey("Exposure_Id");


        // ClaimPayment has one Claim of type Claim
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");

        // ClaimPayment has one Exposure of type Exposure
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Exposure)
            .WithMany()
            .HasForeignKey("Exposure_Id");

        // ClaimPayment has one Beneficiary of type Beneficiary
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Beneficiary)
            .WithMany()
            .HasForeignKey("Beneficiary_Id");

        // ClaimPayment has one ServiceProvider_ of type ServiceProvider_
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.ServiceProvider_)
            .WithMany()
            .HasForeignKey("ServiceProvider__Id");

        // ClaimPayment has one Customer of type Customer
        modelBuilder.Entity<ClaimPayment>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");



        // ServiceProvider_ has one or more Claims of type Claim
        modelBuilder.Entity<Claim>()
            .HasOne<ServiceProvider_>()
            .WithMany(parent => parent.Claims)
            .HasForeignKey("ServiceProvider__Id");

        // ReinsuranceAgreement has one Insurer of type Insurer
        modelBuilder.Entity<ReinsuranceAgreement>()
            .HasOne(x => x.Insurer)
            .WithMany()
            .HasForeignKey("Insurer_Id");


        // ReinsuranceAgreement has one or more Policies of type Policy
        modelBuilder.Entity<Policy>()
            .HasOne<ReinsuranceAgreement>()
            .WithMany(parent => parent.Policies)
            .HasForeignKey("ReinsuranceAgreement_Id");

        // SubrogationRecovery has one Claim of type Claim
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");

        // SubrogationRecovery has one Exposure of type Exposure
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne(x => x.Exposure)
            .WithMany()
            .HasForeignKey("Exposure_Id");

        // SubrogationRecovery has one Counterparty of type ThirdParty
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne(x => x.Counterparty)
            .WithMany()
            .HasForeignKey("Counterparty_Id");



        // ThirdParty has one or more Subrogations of type SubrogationRecovery
        modelBuilder.Entity<SubrogationRecovery>()
            .HasOne<ThirdParty>()
            .WithMany(parent => parent.Subrogations)
            .HasForeignKey("ThirdParty_Id");

        // Document has one Policy of type Policy
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Policy)
            .WithMany()
            .HasForeignKey("Policy_Id");

        // Document has one Claim of type Claim
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Claim)
            .WithMany()
            .HasForeignKey("Claim_Id");

        // Document has one Application of type Application
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Application)
            .WithMany()
            .HasForeignKey("Application_Id");

        // Document has one Customer of type Customer
        modelBuilder.Entity<Document>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


    }
}
