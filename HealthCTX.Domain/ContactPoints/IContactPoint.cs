using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.ContactPoints;

[FhirElement]
[FhirProperty("system", typeof(IContactPointSystem), Cardinality.Optional)]
[FhirProperty("value", typeof(IContactPointValue), Cardinality.Optional)]
[FhirProperty("use", typeof(IContactPointUse), Cardinality.Optional)]
[FhirProperty("rank", typeof(IContactPointRank), Cardinality.Optional)]
[FhirProperty("period", typeof(IContactPointPeriod), Cardinality.Optional)]
public interface IContactPoint : IElement;
