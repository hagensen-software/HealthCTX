using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundle;

[FhirElement]
[FhirProperty("mode", typeof(IBundleEntrySearchMode), Cardinality.Optional)]
[FhirProperty("score", typeof(IBundleEntrySearchScore), Cardinality.Optional)]
public interface IBundleEntrySearch : IElement;
