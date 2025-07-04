using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Bundle;

[FhirResource("Bundle")]
[FhirProperty("identifier", typeof(IBundleIdentifier), Cardinality.Optional)]
[FhirProperty("type", typeof(IBundleType), Cardinality.Mandatory)]
[FhirProperty("timestamp", typeof(IBundleTimestamp), Cardinality.Optional)]
[FhirProperty("total", typeof(IBundleTotal), Cardinality.Optional)]
[FhirProperty("link", typeof(IBundleLink), Cardinality.Multiple)]
[FhirProperty("entry", typeof(IBundleEntry), Cardinality.Multiple)]
[FhirProperty("signature", typeof(IBundleSignature), Cardinality.Optional)]
[FhirProperty("issues", typeof(IBundleIssues), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IBundle : IResource;
