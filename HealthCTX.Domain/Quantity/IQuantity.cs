using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Quantity;

[FhirElement]
[FhirProperty("value", typeof(IQuantityValue), Cardinality.Optional)]
[FhirProperty("comparator", typeof(IQuantityComparator), Cardinality.Optional)]
[FhirProperty("unit", typeof(IQuantityUnit), Cardinality.Optional)]
[FhirProperty("system", typeof(IQuantitySystem), Cardinality.Optional)]
[FhirProperty("code", typeof(IQuantityCode), Cardinality.Optional)]
public interface IQuantity : IElement;
