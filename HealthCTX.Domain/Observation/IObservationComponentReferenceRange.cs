using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

[FhirElement]
[FhirProperty("low", typeof(IObservationReferenceRangeLow), Cardinality.Optional)]
[FhirProperty("high", typeof(IObservationReferenceRangeHigh), Cardinality.Optional)]
[FhirProperty("normalValue", typeof(IObservationReferenceRangeNormalValue), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("type", typeof(IObservationReferenceRangeType), Cardinality.Optional)]
[FhirProperty("appliesTo", typeof(IObservationReferenceRangeAppliesTo), Cardinality.Multiple)]
[FhirProperty("age", typeof(IObservationReferenceRangeAge), Cardinality.Optional)]
public interface IObservationComponentReferenceRange : IElement;
