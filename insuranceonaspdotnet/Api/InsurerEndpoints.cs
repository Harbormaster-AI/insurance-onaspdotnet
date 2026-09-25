
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class InsurerEndpoints
{
    public static IEndpointRouteBuilder MapInsurerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/insurer").WithTags("Insurers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToProducts", AddToProducts);
    group.MapPut("/removeFromProducts", RemoveFromProducts);

    group.MapPut("/addToDistributionPartners", AddToDistributionPartners);
    group.MapPut("/removeFromDistributionPartners", RemoveFromDistributionPartners);

    group.MapPut("/addToPolicies", AddToPolicies);
    group.MapPut("/removeFromPolicies", RemoveFromPolicies);

    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);

    group.MapPut("/addToReinsuranceAgreements", AddToReinsuranceAgreements);
    group.MapPut("/removeFromReinsuranceAgreements", RemoveFromReinsuranceAgreements);


        return app;
    }

    private static async Task<IResult> Create(
        InsurerRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsurer( request );

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
        InsurerRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInsurer( request );

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
        IInsurerService service,
        CancellationToken cancellationToken) {

        var insurer = await service.Get(identifier, cancellationToken);
        return insurer is null ? Results.NotFound() : Results.Ok( insurer );
    }


    private static async Task<IResult> GetAll(
        IInsurerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InsurerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProducts(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProducts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProducts(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProducts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToDistributionPartners(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToDistributionPartners(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromDistributionPartners(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromDistributionPartners(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPolicies(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPolicies(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPolicies(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPolicies(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReinsuranceAgreements(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReinsuranceAgreements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReinsuranceAgreements(
        MultipleAssociationRequest request,
        IInsurerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReinsuranceAgreements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Insurer mapRequestToInsurer( InsurerRequest request ) {
        var model = new Insurer
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            DomicileCountry = request.DomicileCountry,
            NaicNumber = request.NaicNumber,
            Website = request.Website,
        };
        return model;
    }

}
