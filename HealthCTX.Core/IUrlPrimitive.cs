using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IUrlPrimitive : IElement
{
    [FhirIgnore]
    Uri Value { get; init;  }
}
