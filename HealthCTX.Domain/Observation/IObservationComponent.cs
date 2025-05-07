using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

[FhirElement]
[FhirProperty("code", typeof(IObservationComponentCode), Cardinality.Mandatory)]
[FhirProperty("value[Quantity]", typeof(IObservationComponentValueQuantity), Cardinality.Optional)]
[FhirProperty("value[CodeableConcept]", typeof(IObservationComponentValueCodeableConcept), Cardinality.Optional)]
[FhirProperty("value[String]", typeof(IObservationComponentValueString), Cardinality.Optional)]
[FhirProperty("value[Boolean]", typeof(IObservationComponentValueBoolean), Cardinality.Optional)]
[FhirProperty("value[Integer]", typeof(IObservationComponentValueInteger), Cardinality.Optional)]
[FhirProperty("value[Range]", typeof(IObservationComponentValueRange), Cardinality.Optional)]
[FhirProperty("value[Ratio]", typeof(IObservationComponentValueRatio), Cardinality.Optional)]
[FhirProperty("value[SampledData]", typeof(IObservationComponentValueSampledData), Cardinality.Optional)]
[FhirProperty("value[Time]", typeof(IObservationComponentValueTime), Cardinality.Optional)]
[FhirProperty("value[DateTime]", typeof(IObservationComponentValueDateTime), Cardinality.Optional)]
[FhirProperty("value[Period]", typeof(IObservationComponentValuePeriod), Cardinality.Optional)]
[FhirProperty("value[Attachment]", typeof(IObservationComponentValueAttachment), Cardinality.Optional)]
[FhirProperty("value[Reference]", typeof(IObservationComponentValueReference), Cardinality.Optional)]
[FhirProperty("dataAbsentReason", typeof(IObservationComponentDataAbsentReason), Cardinality.Optional)]
[FhirProperty("interpretation", typeof(IObservationComponentInterpretation), Cardinality.Multiple)]
[FhirProperty("referenceRange", typeof(IObservationComponentReferenceRange), Cardinality.Multiple)]
public interface IObservationComponent : IElement;
