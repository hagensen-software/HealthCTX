using HealthCTX.Domain.Framework.Attributes;

namespace HealthCTX.Domain.Framework.Interfaces;

[FhirProperty("id", typeof(IId), Cardinality.Single)]
public interface IElement
{
}
