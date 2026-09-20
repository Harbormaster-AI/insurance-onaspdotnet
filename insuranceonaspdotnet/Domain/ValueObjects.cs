using System.ComponentModel.DataAnnotations.Schema;

namespace insuranceonaspdotnet.Domain;

    [ComplexType]
    public record PolicyNumber(
    string Value
    );

    [ComplexType]
    public record ClaimNumber(
    string Value
    );

    [ComplexType]
    public record Money(
    decimal Amount,
    string Currency
    );

    [ComplexType]
    public record Address(
    string Street,
    string City,
    string State,
    string PostalCode,
    string Country
    );

    [ComplexType]
    public record DateRange(
    DateOnly StartDate,
    DateOnly EndDate
    );

    [ComplexType]
    public record Percentage(
    decimal Value
    );

