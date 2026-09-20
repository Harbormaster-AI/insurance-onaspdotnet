using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class UnderwriterEndpoints
{
    public static IEndpointRouteBuilder MapUnderwriterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/underwriter").WithTags("Underwriters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInsurer", AssignInsurer);
        group.MapPut("/unassignInsurer", UnassignInsurer);

    group.MapPut("/addToDecisions", AddToDecisions);
    group.MapPut("/removeFromDecisions", RemoveFromDecisions);


        return app;
    }

    private static async Task<IResult> Create(
        UnderwriterRequest request,
        IUnderwriterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUnderwriter( request );

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
        UnderwriterRequest request,
        IUnderwriterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToUnderwriter( request );

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
        IUnderwriterService service,
        CancellationToken cancellationToken) {

        var underwriter = await service.Get(identifier, cancellationToken);
        return underwriter is null ? Results.NotFound() : Results.Ok( underwriter );
    }


    private static async Task<IResult> GetAll(
        IUnderwriterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( UnderwriterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IUnderwriterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInsurer(
        AssociationRequest request,
        IUnderwriterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInsurer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInsurer(
    AssociationRequest request,
    IUnderwriterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInsurer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToDecisions(
        MultipleAssociationRequest request,
        IUnderwriterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDecisions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDecisions(
        MultipleAssociationRequest request,
        IUnderwriterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDecisions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Underwriter mapRequestToUnderwriter( UnderwriterRequest request ) {
        var model = new Underwriter
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmployeeId = request.EmployeeId,
            AuthorityLimit = request.AuthorityLimit,
        };
        return model;
    }

}
