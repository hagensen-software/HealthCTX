using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirProperty("id", typeof(IId), Cardinality.Optional)]
public interface IElement
{
}
