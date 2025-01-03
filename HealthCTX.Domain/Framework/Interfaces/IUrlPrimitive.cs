using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirPrimitive]
public interface IUrlPrimitive : IElement
{
    [FhirIgnore]
    Uri Value { get; init;  }
}
