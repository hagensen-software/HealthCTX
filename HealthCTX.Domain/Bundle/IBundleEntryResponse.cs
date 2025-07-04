using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.ResourceContents;

namespace HealthCTX.Domain.Bundle;

[FhirElement]
[FhirProperty("status", typeof(IBundleEntryResponseStatus), Cardinality.Mandatory)]
[FhirProperty("location", typeof(IBundleEntryResponseLocation), Cardinality.Optional)]
[FhirProperty("etag", typeof(IBundleEntryResponseEtag), Cardinality.Optional)]
[FhirProperty("lastModified", typeof(IBundleEntryResponseLastModified), Cardinality.Optional)]
[FhirProperty("outcome", typeof(IBundleEntryResponseOutcome), Cardinality.Optional)]
public interface IBundleEntryResponse : IElement;
