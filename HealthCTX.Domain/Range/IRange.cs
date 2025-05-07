using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Range;

[FhirElement]
[FhirProperty("low", typeof(IRangeLow), Cardinality.Optional)]
[FhirProperty("high", typeof(IRangeHigh), Cardinality.Optional)]
public interface IRange : IElement;
