
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class BillingAccountEndpoints
{
    public static IEndpointRouteBuilder MapBillingAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/billingAccount").WithTags("BillingAccounts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToInvoices", AddToInvoices);
    group.MapPut("/removeFromInvoices", RemoveFromInvoices);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);


        return app;
    }

    private static async Task<IResult> Create(
        BillingAccountRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBillingAccount( request );

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
        BillingAccountRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToBillingAccount( request );

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
        IBillingAccountService service,
        CancellationToken cancellationToken) {

        var billingAccount = await service.Get(identifier, cancellationToken);
        return billingAccount is null ? Results.NotFound() : Results.Ok( billingAccount );
    }


    private static async Task<IResult> GetAll(
        IBillingAccountService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( BillingAccountResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IBillingAccountService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInvoices(
        MultipleAssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInvoices(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInvoices(
        MultipleAssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInvoices(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        IBillingAccountService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static BillingAccount mapRequestToBillingAccount( BillingAccountRequest request ) {
        var model = new BillingAccount
        {
            Id = request.Id,
            AccountNumber = request.AccountNumber,
            Balance = request.Balance,
            Status = request.Status,
        };
        return model;
    }

}
