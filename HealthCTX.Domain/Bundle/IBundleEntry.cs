using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.ResourceContents;

namespace HealthCTX.Domain.Bundle;

public interface IResourceType : IStringPrimitive;

[FhirElement]
[FhirProperty("link", typeof(IBundleLink), Cardinality.Multiple)]
[FhirProperty("fullUrl", typeof(IBundleEntryFullUrl), Cardinality.Optional)]
[FhirProperty("resource", typeof(IBundleEntryResource), Cardinality.Optional)]
[FhirProperty("search", typeof(IBundleEntrySearch), Cardinality.Optional)]
[FhirProperty("request", typeof(IBundleEntryRequest), Cardinality.Optional)]
[FhirProperty("response", typeof(IBundleEntryResponse), Cardinality.Optional)]
public interface IBundleEntry : IElement;
