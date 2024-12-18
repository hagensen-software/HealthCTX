using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.ContactPoints;

[FhirElement]
[FhirProperty("system", typeof(IContactPointSystem), Cardinality.Single)]
[FhirProperty("value", typeof(IContactPointValue), Cardinality.Single)]
[FhirProperty("use", typeof(IContactPointUse), Cardinality.Single)]
[FhirProperty("rank", typeof(IContactPointRank), Cardinality.Single)]
[FhirProperty("period", typeof(IContactPointPeriod), Cardinality.Single)]
public interface IContactPoint : IElement;
