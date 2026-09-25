
using insuranceonaspdotnet.Service;
using insuranceonaspdotnet.Domain;
using insuranceonaspdotnet.Contracts;

namespace insuranceonaspdotnet.Api;

public static class PolicyEndpoints
{
    public static IEndpointRouteBuilder MapPolicyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/policy").WithTags("Policys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignInsurer", AssignInsurer);
        group.MapPut("/unassignInsurer", UnassignInsurer);
        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignProduct", AssignProduct);
        group.MapPut("/unassignProduct", UnassignProduct);
        group.MapPut("/assignAgent", AssignAgent);
        group.MapPut("/unassignAgent", UnassignAgent);
        group.MapPut("/assignBillingAccount", AssignBillingAccount);
        group.MapPut("/unassignBillingAccount", UnassignBillingAccount);

    group.MapPut("/addToCoverages", AddToCoverages);
    group.MapPut("/removeFromCoverages", RemoveFromCoverages);

    group.MapPut("/addToInsuredObjects", AddToInsuredObjects);
    group.MapPut("/removeFromInsuredObjects", RemoveFromInsuredObjects);

    group.MapPut("/addToEndorsements", AddToEndorsements);
    group.MapPut("/removeFromEndorsements", RemoveFromEndorsements);

    group.MapPut("/addToBeneficiaries", AddToBeneficiaries);
    group.MapPut("/removeFromBeneficiaries", RemoveFromBeneficiaries);

    group.MapPut("/addToClaims", AddToClaims);
    group.MapPut("/removeFromClaims", RemoveFromClaims);

    group.MapPut("/addToReinsuranceAgreements", AddToReinsuranceAgreements);
    group.MapPut("/removeFromReinsuranceAgreements", RemoveFromReinsuranceAgreements);


        return app;
    }

    private static async Task<IResult> Create(
        PolicyRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicy( request );

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
        PolicyRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPolicy( request );

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
        IPolicyService service,
        CancellationToken cancellationToken) {

        var policy = await service.Get(identifier, cancellationToken);
        return policy is null ? Results.NotFound() : Results.Ok( policy );
    }


    private static async Task<IResult> GetAll(
        IPolicyService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PolicyResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInsurer(
        AssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInsurer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInsurer(
    AssociationRequest request,
    IPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInsurer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProduct(
        AssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProduct(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProduct(
    AssociationRequest request,
    IPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProduct(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignAgent(
        AssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignAgent(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignAgent(
    AssociationRequest request,
    IPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignAgent(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBillingAccount(
        AssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBillingAccount(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBillingAccount(
    AssociationRequest request,
    IPolicyService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBillingAccount(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCoverages(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCoverages(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCoverages(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCoverages(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInsuredObjects(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInsuredObjects(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInsuredObjects(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInsuredObjects(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToEndorsements(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToEndorsements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromEndorsements(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromEndorsements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBeneficiaries(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBeneficiaries(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBeneficiaries(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBeneficiaries(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToClaims(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToClaims(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromClaims(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromClaims(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReinsuranceAgreements(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReinsuranceAgreements(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReinsuranceAgreements(
        MultipleAssociationRequest request,
        IPolicyService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReinsuranceAgreements(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Policy mapRequestToPolicy( PolicyRequest request ) {
        var model = new Policy
        {
            Id = request.Id,
            PolicyNumber = request.PolicyNumber,
            EffectivePeriod = request.EffectivePeriod,
            TotalPremium = request.TotalPremium,
            Status = request.Status,
            PaymentPlan = request.PaymentPlan,
        };
        return model;
    }

}
