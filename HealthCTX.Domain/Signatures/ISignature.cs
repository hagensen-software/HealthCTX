using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Signatures;

[FhirElement]
[FhirProperty("type", typeof(ISignatureType), Cardinality.Multiple)]
[FhirProperty("when", typeof(ISignatureWhen), Cardinality.Optional)]
[FhirProperty("who", typeof(ISignatureWho), Cardinality.Optional)]
[FhirProperty("onBehalfOf", typeof(ISignatureOnBehalfOf), Cardinality.Optional)]
[FhirProperty("targetFormat", typeof(ISignatureTargetFormat), Cardinality.Optional)]
[FhirProperty("sigFormat", typeof(ISignatureSigFormat), Cardinality.Optional)]
[FhirProperty("data", typeof(ISignatureData), Cardinality.Optional)]
public interface ISignature : IElement;
