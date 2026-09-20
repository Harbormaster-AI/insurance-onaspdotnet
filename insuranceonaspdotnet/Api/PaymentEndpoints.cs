using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payment").WithTags("Payments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInvoice", AssignInvoice);
        group.MapPut("/unassignInvoice", UnassignInvoice);
        group.MapPut("/assignBillingAccount", AssignBillingAccount);
        group.MapPut("/unassignBillingAccount", UnassignBillingAccount);
        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayment( request );

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
        PaymentRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayment( request );

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
        IPaymentService service,
        CancellationToken cancellationToken) {

        var payment = await service.Get(identifier, cancellationToken);
        return payment is null ? Results.NotFound() : Results.Ok( payment );
    }


    private static async Task<IResult> GetAll(
        IPaymentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInvoice(
        AssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInvoice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInvoice(
    AssociationRequest request,
    IPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInvoice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBillingAccount(
        AssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBillingAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBillingAccount(
    AssociationRequest request,
    IPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBillingAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Payment mapRequestToPayment( PaymentRequest request ) {
        var model = new Payment
        {
            Id = request.Id,
            PaymentReference = request.PaymentReference,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            Method = request.Method,
            Status = request.Status,
        };
        return model;
    }

}
