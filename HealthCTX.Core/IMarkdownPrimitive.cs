using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IMarkdownPrimitive : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
