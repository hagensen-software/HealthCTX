using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirProperty("id", typeof(IId), Cardinality.Optional)]
public interface IElement
{
}
