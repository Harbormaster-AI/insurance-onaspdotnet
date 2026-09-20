using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ClaimPaymentEndpoints
{
    public static IEndpointRouteBuilder MapClaimPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/claimPayment").WithTags("ClaimPayments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignClaim", AssignClaim);
        group.MapPut("/unassignClaim", UnassignClaim);
        group.MapPut("/assignExposure", AssignExposure);
        group.MapPut("/unassignExposure", UnassignExposure);
        group.MapPut("/assignBeneficiary", AssignBeneficiary);
        group.MapPut("/unassignBeneficiary", UnassignBeneficiary);
        group.MapPut("/assignServiceProvider_", AssignServiceProvider_);
        group.MapPut("/unassignServiceProvider_", UnassignServiceProvider_);
        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);


        return app;
    }

    private static async Task<IResult> Create(
        ClaimPaymentRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaimPayment( request );

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
        ClaimPaymentRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaimPayment( request );

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
        IClaimPaymentService service,
        CancellationToken cancellationToken) {

        var claimPayment = await service.Get(identifier, cancellationToken);
        return claimPayment is null ? Results.NotFound() : Results.Ok( claimPayment );
    }


    private static async Task<IResult> GetAll(
        IClaimPaymentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ClaimPaymentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignClaim(
        AssociationRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignClaim(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignClaim(
    AssociationRequest request,
    IClaimPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignClaim(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignExposure(
        AssociationRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignExposure(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignExposure(
    AssociationRequest request,
    IClaimPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignExposure(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBeneficiary(
        AssociationRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBeneficiary(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBeneficiary(
    AssociationRequest request,
    IClaimPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBeneficiary(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignServiceProvider_(
        AssociationRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignServiceProvider_(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignServiceProvider_(
    AssociationRequest request,
    IClaimPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignServiceProvider_(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IClaimPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IClaimPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ClaimPayment mapRequestToClaimPayment( ClaimPaymentRequest request ) {
        var model = new ClaimPayment
        {
            Id = request.Id,
            PaymentNumber = request.PaymentNumber,
            Amount = request.Amount,
            PaymentDate = request.PaymentDate,
            PayeeType = request.PayeeType,
            Method = request.Method,
            Status = request.Status,
        };
        return model;
    }

}
