using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class QuoteEndpoints
{
    public static IEndpointRouteBuilder MapQuoteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/quote").WithTags("Quotes");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignApplication", AssignApplication);
        group.MapPut("/unassignApplication", UnassignApplication);
        group.MapPut("/assignPolicy", AssignPolicy);
        group.MapPut("/unassignPolicy", UnassignPolicy);

    group.MapPut("/addToUnderwritingDecisions", AddToUnderwritingDecisions);
    group.MapPut("/removeFromUnderwritingDecisions", RemoveFromUnderwritingDecisions);


        return app;
    }

    private static async Task<IResult> Create(
        QuoteRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToQuote( request );

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
        QuoteRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToQuote( request );

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
        IQuoteService service,
        CancellationToken cancellationToken) {

        var quote = await service.Get(identifier, cancellationToken);
        return quote is null ? Results.NotFound() : Results.Ok( quote );
    }


    private static async Task<IResult> GetAll(
        IQuoteService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( QuoteResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignApplication(
        AssociationRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignApplication(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignApplication(
    AssociationRequest request,
    IQuoteService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignApplication(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPolicy(
        AssociationRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPolicy(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPolicy(
    AssociationRequest request,
    IQuoteService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPolicy(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToUnderwritingDecisions(
        MultipleAssociationRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToUnderwritingDecisions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromUnderwritingDecisions(
        MultipleAssociationRequest request,
        IQuoteService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromUnderwritingDecisions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Quote mapRequestToQuote( QuoteRequest request ) {
        var model = new Quote
        {
            Id = request.Id,
            QuoteNumber = request.QuoteNumber,
            TotalPremium = request.TotalPremium,
            RatingDate = request.RatingDate,
            AsBound = request.AsBound,
        };
        return model;
    }

}
