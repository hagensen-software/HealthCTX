using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Ratio;

[FhirElement]
[FhirProperty("numerator", typeof(IRatioNumerator), Cardinality.Optional)]
[FhirProperty("denominator", typeof(IRatioDenominator), Cardinality.Optional)]
public interface IRatio : IElement;
