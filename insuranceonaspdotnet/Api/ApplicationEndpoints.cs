using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class ApplicationEndpoints
{
    public static IEndpointRouteBuilder MapApplicationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/application").WithTags("Applications");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignProduct", AssignProduct);
        group.MapPut("/unassignProduct", UnassignProduct);
        group.MapPut("/assignDistributor", AssignDistributor);
        group.MapPut("/unassignDistributor", UnassignDistributor);
        group.MapPut("/assignSelectedQuote", AssignSelectedQuote);
        group.MapPut("/unassignSelectedQuote", UnassignSelectedQuote);

    group.MapPut("/addToQuotes", AddToQuotes);
    group.MapPut("/removeFromQuotes", RemoveFromQuotes);


        return app;
    }

    private static async Task<IResult> Create(
        ApplicationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToApplication( request );

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
        ApplicationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToApplication( request );

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
        IApplicationService service,
        CancellationToken cancellationToken) {

        var application = await service.Get(identifier, cancellationToken);
        return application is null ? Results.NotFound() : Results.Ok( application );
    }


    private static async Task<IResult> GetAll(
        IApplicationService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ApplicationResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProduct(
        AssociationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProduct(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProduct(
    AssociationRequest request,
    IApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProduct(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDistributor(
        AssociationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDistributor(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDistributor(
    AssociationRequest request,
    IApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDistributor(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSelectedQuote(
        AssociationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSelectedQuote(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSelectedQuote(
    AssociationRequest request,
    IApplicationService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSelectedQuote(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToQuotes(
        MultipleAssociationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToQuotes(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromQuotes(
        MultipleAssociationRequest request,
        IApplicationService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromQuotes(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Application mapRequestToApplication( ApplicationRequest request ) {
        var model = new Application
        {
            Id = request.Id,
            ApplicationNumber = request.ApplicationNumber,
            SubmissionDate = request.SubmissionDate,
            Status = request.Status,
        };
        return model;
    }

}
