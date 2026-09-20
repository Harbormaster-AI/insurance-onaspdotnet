using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class UnderwritingDecisionEndpoints
{
    public static IEndpointRouteBuilder MapUnderwritingDecisionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/underwritingDecision").WithTags("UnderwritingDecisions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignQuote", AssignQuote);
        group.MapPut("/unassignQuote", UnassignQuote);
        group.MapPut("/assignUnderwriter", AssignUnderwriter);
        group.MapPut("/unassignUnderwriter", UnassignUnderwriter);


        return app;
    }

    private static async Task<IResult> Create(
        UnderwritingDecisionRequest request,
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUnderwritingDecision( request );

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
        UnderwritingDecisionRequest request,
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUnderwritingDecision( request );

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
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {

        var underwritingDecision = await service.Get(identifier, cancellationToken);
        return underwritingDecision is null ? Results.NotFound() : Results.Ok( underwritingDecision );
    }


    private static async Task<IResult> GetAll(
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( UnderwritingDecisionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignQuote(
        AssociationRequest request,
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignQuote(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignQuote(
    AssociationRequest request,
    IUnderwritingDecisionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignQuote(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignUnderwriter(
        AssociationRequest request,
        IUnderwritingDecisionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignUnderwriter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignUnderwriter(
    AssociationRequest request,
    IUnderwritingDecisionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignUnderwriter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static UnderwritingDecision mapRequestToUnderwritingDecision( UnderwritingDecisionRequest request ) {
        var model = new UnderwritingDecision
        {
            Id = request.Id,
            Notes = request.Notes,
            DecisionDate = request.DecisionDate,
            Decision = request.Decision,
        };
        return model;
    }

}
