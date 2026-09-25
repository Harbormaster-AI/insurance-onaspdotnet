
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customer").WithTags("Customers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToApplications", AddToApplications);
    group.MapPut("/removeFromApplications", RemoveFromApplications);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);

    group.MapPut("/addToAgents", AddToAgents);
    group.MapPut("/removeFromAgents", RemoveFromAgents);

    group.MapPut("/addToBeneficiaries", AddToBeneficiaries);
    group.MapPut("/removeFromBeneficiaries", RemoveFromBeneficiaries);


        return app;
    }

    private static async Task<IResult> Create(
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var customer = await service.Get(identifier, cancellationToken);
        return customer is null ? Results.NotFound() : Results.Ok( customer );
    }


    private static async Task<IResult> GetAll(
        ICustomerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CustomerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToApplications(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApplications(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApplications(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApplications(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAgents(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAgents(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAgents(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAgents(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBeneficiaries(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBeneficiaries(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBeneficiaries(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBeneficiaries(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Customer mapRequestToCustomer( CustomerRequest request ) {
        var model = new Customer
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            OrganizationName = request.OrganizationName,
            TaxId = request.TaxId,
            DateOfBirth = request.DateOfBirth,
            PrimaryAddress = request.PrimaryAddress,
            CustomerType = request.CustomerType,
        };
        return model;
    }

}
