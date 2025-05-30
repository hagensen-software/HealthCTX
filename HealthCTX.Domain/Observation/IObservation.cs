﻿using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

[FhirResource("Observation")]
[FhirProperty("identifier", typeof(IObservationIdentifier), Cardinality.Multiple)]
[FhirProperty("instantiates[Canonical]", typeof(IObservationInstantiatesCanonical), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("instantiates[Reference]", typeof(IObservationInstantiatesReference), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("basedOn", typeof(IObservationBasedOn), Cardinality.Multiple)]
[FhirProperty("triggeredBy", typeof(IObservationTriggeredBy), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("partOf", typeof(IObservationPartOf), Cardinality.Multiple)]
[FhirProperty("status", typeof(IObservationStatus), Cardinality.Mandatory)]
[FhirProperty("category", typeof(IObservationCategory), Cardinality.Multiple)]
[FhirProperty("code", typeof(IObservationCode), Cardinality.Mandatory)]
[FhirProperty("subject", typeof(IObservationSubject), Cardinality.Optional)]
[FhirProperty("focus", typeof(IObservationFocus), Cardinality.Multiple)]
[FhirProperty("encounter", typeof(IObservationEncounter), Cardinality.Optional)]
[FhirProperty("effective[DateTime]", typeof(IObservationEffectiveDateTime), Cardinality.Optional)]
[FhirProperty("effective[Period]", typeof(IObservationEffectivePeriod), Cardinality.Optional)]
[FhirProperty("effective[Timing]", typeof(IObservationEffectiveTiming), Cardinality.Optional)]
[FhirProperty("effective[Instant]", typeof(IObservationEffectiveInstant), Cardinality.Optional)]
[FhirProperty("issued", typeof(IObservationIssued), Cardinality.Optional)]
[FhirProperty("performer", typeof(IObservationPerformer), Cardinality.Multiple)]
[FhirProperty("value[Quantity]", typeof(IObservationValueQuantity), Cardinality.Optional)]
[FhirProperty("value[CodeableConcept]", typeof(IObservationValueCodeableConcept), Cardinality.Optional)]
[FhirProperty("value[String]", typeof(IObservationValueString), Cardinality.Optional)]
[FhirProperty("value[Boolean]", typeof(IObservationValueBoolean), Cardinality.Optional)]
[FhirProperty("value[Integer]", typeof(IObservationValueInteger), Cardinality.Optional)]
[FhirProperty("value[Range]", typeof(IObservationValueRange), Cardinality.Optional)]
[FhirProperty("value[Ratio]", typeof(IObservationValueRatio), Cardinality.Optional)]
[FhirProperty("value[SampledData]", typeof(IObservationValueSampledData), Cardinality.Optional)]
[FhirProperty("value[Time]", typeof(IObservationValueTime), Cardinality.Optional)]
[FhirProperty("value[DateTime]", typeof(IObservationValueDateTime), Cardinality.Optional)]
[FhirProperty("value[Period]", typeof(IObservationValuePeriod), Cardinality.Optional)]
[FhirProperty("value[Attachment]", typeof(IObservationValueAttachment), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("value[Reference]", typeof(IObservationValueReference), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("dataAbsentReason", typeof(IObservationDataAbsentReason), Cardinality.Optional)]
[FhirProperty("interpretation", typeof(IObservationInterpretation), Cardinality.Multiple)]
[FhirProperty("note", typeof(IObservationNote), Cardinality.Multiple)]
[FhirProperty("bodySite", typeof(IObservationBodySite), Cardinality.Optional)]
[FhirProperty("bodyStructure", typeof(IObservationBodyStructure), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("method", typeof(IObservationMethod), Cardinality.Optional)]
[FhirProperty("specimen", typeof(IObservationSpecimen), Cardinality.Optional)]
[FhirProperty("device", typeof(IObservationDevice), Cardinality.Optional)]
[FhirProperty("referenceRange", typeof(IObservationReferenceRange), Cardinality.Multiple)]
[FhirProperty("hasMember", typeof(IObservationHasMember), Cardinality.Multiple)]
[FhirProperty("derivedFrom", typeof(IObservationDerivedFrom), Cardinality.Multiple)]
[FhirProperty("component", typeof(IObservationComponent), Cardinality.Multiple)]
public interface IObservation : IResource;
