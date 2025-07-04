using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundle;

[FhirElement]
[FhirProperty("method", typeof(IBundleEntryRequestMethod), Cardinality.Mandatory)]
[FhirProperty("url", typeof(IBundleEntryRequestUrl), Cardinality.Mandatory)]
[FhirProperty("ifNoneMatch", typeof(IBundleEntryRequestIfNoneMatch), Cardinality.Optional)]
[FhirProperty("ifModifiedSince", typeof(IBundleEntryRequestIfModifiedSince), Cardinality.Optional)]
[FhirProperty("ifMatch", typeof(IBundleEntryRequestIfMatch), Cardinality.Optional)]
[FhirProperty("ifNoneExist", typeof(IBundleEntryRequestIfNoneExist), Cardinality.Optional)]
public interface IBundleEntryRequest : IElement;
