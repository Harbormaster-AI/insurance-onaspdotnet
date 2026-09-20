using insuranceonaspdotnet.Api;
using insuranceonaspdotnet.Persistence;
using insuranceonaspdotnet.Service;

using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();


var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("insuranceonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

builder.Services.AddScoped<IInsurerRepository, InsurerRepository>();
builder.Services.AddScoped<IInsuranceProductRepository, InsuranceProductRepository>();
builder.Services.AddScoped<ICoverageDefinitionRepository, CoverageDefinitionRepository>();
builder.Services.AddScoped<IDistributorRepository, DistributorRepository>();
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IUnderwritingDecisionRepository, UnderwritingDecisionRepository>();
builder.Services.AddScoped<IUnderwriterRepository, UnderwriterRepository>();
builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();
builder.Services.AddScoped<IEndorsementRepository, EndorsementRepository>();
builder.Services.AddScoped<IPolicyCoverageRepository, PolicyCoverageRepository>();
builder.Services.AddScoped<IInsuredObjectRepository, InsuredObjectRepository>();
builder.Services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
builder.Services.AddScoped<IBillingAccountRepository, BillingAccountRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IExposureRepository, ExposureRepository>();
builder.Services.AddScoped<IAdjusterRepository, AdjusterRepository>();
builder.Services.AddScoped<IClaimReserveRepository, ClaimReserveRepository>();
builder.Services.AddScoped<IClaimPaymentRepository, ClaimPaymentRepository>();
builder.Services.AddScoped<IServiceProviderRepository, ServiceProviderRepository>();
builder.Services.AddScoped<IReinsuranceAgreementRepository, ReinsuranceAgreementRepository>();
builder.Services.AddScoped<ISubrogationRecoveryRepository, SubrogationRecoveryRepository>();
builder.Services.AddScoped<IThirdPartyRepository, ThirdPartyRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

builder.Services.AddScoped<IInsurerService, InsurerService>();
builder.Services.AddScoped<IInsuranceProductService, InsuranceProductService>();
builder.Services.AddScoped<ICoverageDefinitionService, CoverageDefinitionService>();
builder.Services.AddScoped<IDistributorService, DistributorService>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IUnderwritingDecisionService, UnderwritingDecisionService>();
builder.Services.AddScoped<IUnderwriterService, UnderwriterService>();
builder.Services.AddScoped<IPolicyService, PolicyService>();
builder.Services.AddScoped<IEndorsementService, EndorsementService>();
builder.Services.AddScoped<IPolicyCoverageService, PolicyCoverageService>();
builder.Services.AddScoped<IInsuredObjectService, InsuredObjectService>();
builder.Services.AddScoped<IBeneficiaryService, BeneficiaryService>();
builder.Services.AddScoped<IBillingAccountService, BillingAccountService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<IExposureService, ExposureService>();
builder.Services.AddScoped<IAdjusterService, AdjusterService>();
builder.Services.AddScoped<IClaimReserveService, ClaimReserveService>();
builder.Services.AddScoped<IClaimPaymentService, ClaimPaymentService>();
builder.Services.AddScoped<IServiceProviderService, ServiceProviderService>();
builder.Services.AddScoped<IReinsuranceAgreementService, ReinsuranceAgreementService>();
builder.Services.AddScoped<ISubrogationRecoveryService, SubrogationRecoveryService>();
builder.Services.AddScoped<IThirdPartyService, ThirdPartyService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

var app = builder.Build();

// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapInsurerEndpoints();
app.MapInsuranceProductEndpoints();
app.MapCoverageDefinitionEndpoints();
app.MapDistributorEndpoints();
app.MapAgentEndpoints();
app.MapCustomerEndpoints();
app.MapApplicationEndpoints();
app.MapQuoteEndpoints();
app.MapUnderwritingDecisionEndpoints();
app.MapUnderwriterEndpoints();
app.MapPolicyEndpoints();
app.MapEndorsementEndpoints();
app.MapPolicyCoverageEndpoints();
app.MapInsuredObjectEndpoints();
app.MapBeneficiaryEndpoints();
app.MapBillingAccountEndpoints();
app.MapInvoiceEndpoints();
app.MapPaymentEndpoints();
app.MapClaimEndpoints();
app.MapIncidentEndpoints();
app.MapExposureEndpoints();
app.MapAdjusterEndpoints();
app.MapClaimReserveEndpoints();
app.MapClaimPaymentEndpoints();
app.MapServiceProviderEndpoints();
app.MapReinsuranceAgreementEndpoints();
app.MapSubrogationRecoveryEndpoints();
app.MapThirdPartyEndpoints();
app.MapDocumentEndpoints();

app.Run();

public partial class Program { }

