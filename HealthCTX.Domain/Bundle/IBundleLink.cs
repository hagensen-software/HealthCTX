using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundle;

[FhirElement]
[FhirProperty("relation", typeof(IBundleLinkRelation), Cardinality.Mandatory)]
[FhirProperty("url", typeof(IBundleLinkUrl), Cardinality.Mandatory)]
public interface IBundleLink : IElement;
