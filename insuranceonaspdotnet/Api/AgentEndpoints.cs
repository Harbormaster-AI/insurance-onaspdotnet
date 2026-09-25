
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class AgentEndpoints
{
    public static IEndpointRouteBuilder MapAgentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/agent").WithTags("Agents");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignDistributor", AssignDistributor);
        group.MapPut("/unassignDistributor", UnassignDistributor);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToCustomers", AddToCustomers);
    group.MapPut("/removeFromCustomers", RemoveFromCustomers);


        return app;
    }

    private static async Task<IResult> Create(
        AgentRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAgent( request );

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
        AgentRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToAgent( request );

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
        IAgentService service,
        CancellationToken cancellationToken) {

        var agent = await service.Get(identifier, cancellationToken);
        return agent is null ? Results.NotFound() : Results.Ok( agent );
    }


    private static async Task<IResult> GetAll(
        IAgentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( AgentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IAgentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDistributor(
        AssociationRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDistributor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDistributor(
    AssociationRequest request,
    IAgentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDistributor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCustomers(
        MultipleAssociationRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCustomers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCustomers(
        MultipleAssociationRequest request,
        IAgentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCustomers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Agent mapRequestToAgent( AgentRequest request ) {
        var model = new Agent
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            LicenseId = request.LicenseId,
            Status = request.Status,
        };
        return model;
    }

}
