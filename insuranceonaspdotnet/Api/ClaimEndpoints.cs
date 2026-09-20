using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ClaimEndpoints
{
    public static IEndpointRouteBuilder MapClaimEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/claim").WithTags("Claims");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);
        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignAdjuster", AssignAdjuster);
        group.MapPut("/unassignAdjuster", UnassignAdjuster);
        group.MapPut("/assignIncident", AssignIncident);
        group.MapPut("/unassignIncident", UnassignIncident);

    group.MapPut("/addToExposures", AddToExposures);
    group.MapPut("/removeFromExposures", RemoveFromExposures);

    group.MapPut("/addToReserves", AddToReserves);
    group.MapPut("/removeFromReserves", RemoveFromReserves);

    group.MapPut("/addToClaimPayments", AddToClaimPayments);
    group.MapPut("/removeFromClaimPayments", RemoveFromClaimPayments);

    group.MapPut("/addToServiceProviders", AddToServiceProviders);
    group.MapPut("/removeFromServiceProviders", RemoveFromServiceProviders);

    group.MapPut("/addToSubrogations", AddToSubrogations);
    group.MapPut("/removeFromSubrogations", RemoveFromSubrogations);


        return app;
    }

    private static async Task<IResult> Create(
        ClaimRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaim( request );

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
        ClaimRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToClaim( request );

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
        IClaimService service,
        CancellationToken cancellationToken) {

        var claim = await service.Get(identifier, cancellationToken);
        return claim is null ? Results.NotFound() : Results.Ok( claim );
    }


    private static async Task<IResult> GetAll(
        IClaimService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ClaimResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IClaimService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAdjuster(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAdjuster(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAdjuster(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAdjuster(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignIncident(
        AssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignIncident(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignIncident(
    AssociationRequest request,
    IClaimService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignIncident(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToExposures(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToExposures(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromExposures(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromExposures(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReserves(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReserves(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReserves(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReserves(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToClaimPayments(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaimPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaimPayments(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaimPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToServiceProviders(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToServiceProviders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromServiceProviders(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromServiceProviders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSubrogations(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSubrogations(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSubrogations(
        MultipleAssociationRequest request,
        IClaimService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSubrogations(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Claim mapRequestToClaim( ClaimRequest request ) {
        var model = new Claim
        {
            Id = request.Id,
            ClaimNumber = request.ClaimNumber,
            NoticeDate = request.NoticeDate,
            LossDate = request.LossDate,
            ReportedBy = request.ReportedBy,
            Status = request.Status,
            LossCause = request.LossCause,
        };
        return model;
    }

}
