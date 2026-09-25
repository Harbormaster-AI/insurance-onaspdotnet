
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ReinsuranceAgreementEndpoints
{
    public static IEndpointRouteBuilder MapReinsuranceAgreementEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/reinsuranceAgreement").WithTags("ReinsuranceAgreements");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInsurer", AssignInsurer);
        group.MapPut("/unassignInsurer", UnassignInsurer);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);


        return app;
    }

    private static async Task<IResult> Create(
        ReinsuranceAgreementRequest request,
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReinsuranceAgreement( request );

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
        ReinsuranceAgreementRequest request,
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReinsuranceAgreement( request );

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
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {

        var reinsuranceAgreement = await service.Get(identifier, cancellationToken);
        return reinsuranceAgreement is null ? Results.NotFound() : Results.Ok( reinsuranceAgreement );
    }


    private static async Task<IResult> GetAll(
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ReinsuranceAgreementResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInsurer(
        AssociationRequest request,
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInsurer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInsurer(
    AssociationRequest request,
    IReinsuranceAgreementService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInsurer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IReinsuranceAgreementService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ReinsuranceAgreement mapRequestToReinsuranceAgreement( ReinsuranceAgreementRequest request ) {
        var model = new ReinsuranceAgreement
        {
            Id = request.Id,
            AgreementNumber = request.AgreementNumber,
            EffectivePeriod = request.EffectivePeriod,
            Retention = request.Retention,
            Limit = request.Limit,
            CessionPercentage = request.CessionPercentage,
            ReinsuranceType = request.ReinsuranceType,
            TreatyType = request.TreatyType,
        };
        return model;
    }

}
