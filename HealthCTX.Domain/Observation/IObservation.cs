using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

/// <summary>
///     <para>Interface for the HL7 FHIR Observation resource.</para>
/// </summary>
/// <remarks>
///     <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
///     <list type="table">
///         <item>
///             <term>identifier</term>
///             <description><see cref="IObservationIdentifier"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>instantiates[Canonical]</term>
///             <description><see cref="IObservationInstantiatesCanonical"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>instantiates[Reference]</term>
///             <description><see cref="IObservationInstantiatesReference"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>basedOn</term>
///             <description><see cref="IObservationBasedOn"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>triggeredBy</term>
///             <description><see cref="IObservationTriggeredBy"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>partOf</term>
///             <description><see cref="IObservationPartOf"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>status</term>
///             <description><see cref="IObservationStatus"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>category</term>
///             <description><see cref="IObservationCategory"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>code</term>
///             <description><see cref="IObservationCode"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>subject</term>
///             <description><see cref="IObservationSubject"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>focus</term>
///             <description><see cref="IObservationFocus"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>encounter</term>
///             <description><see cref="IObservationEncounter"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>effective[DateTime]</term>
///             <description><see cref="IObservationEffectiveDateTime"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>effective[Period]</term>
///             <description><see cref="IObservationEffectivePeriod"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>effective[Timing]</term>
///             <description><see cref="IObservationEffectiveTiming"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>effective[Instant]</term>
///             <description><see cref="IObservationEffectiveInstant"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>issued</term>
///             <description><see cref="IObservationIssued"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>performer</term>
///             <description><see cref="IObservationPerformer"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Quantity]</term>
///             <description><see cref="IObservationValueQuantity"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[CodeableConcept]</term>
///             <description><see cref="IObservationValueCodeableConcept"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[String]</term>
///             <description><see cref="IObservationValueString"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Boolean]</term>
///             <description><see cref="IObservationValueBoolean"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Integer]</term>
///             <description><see cref="IObservationValueInteger"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Range]</term>
///             <description><see cref="IObservationValueRange"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Ratio]</term>
///             <description><see cref="IObservationValueRatio"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[SampledData]</term>
///             <description><see cref="IObservationValueSampledData"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Time]</term>
///             <description><see cref="IObservationValueTime"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[DateTime]</term>
///             <description><see cref="IObservationValueDateTime"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Period]</term>
///             <description><see cref="IObservationValuePeriod"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>value[Attachment]</term>
///             <description><see cref="IObservationValueAttachment"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>value[Reference]</term>
///             <description><see cref="IObservationValueReference"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>dataAbsentReason</term>
///             <description><see cref="IObservationDataAbsentReason"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>interpretation</term>
///             <description><see cref="IObservationInterpretation"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>note</term>
///             <description><see cref="IObservationNote"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>bodySite</term>
///             <description><see cref="IObservationBodySite"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>bodyStructure</term>
///             <description><see cref="IObservationBodyStructure"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>method</term>
///             <description><see cref="IObservationMethod"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>specimen</term>
///             <description><see cref="IObservationSpecimen"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>device</term>
///             <description><see cref="IObservationDevice"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>referenceRange</term>
///             <description><see cref="IObservationReferenceRange"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>hasMember</term>
///             <description><see cref="IObservationHasMember"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>derivedFrom</term>
///             <description><see cref="IObservationDerivedFrom"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>component</term>
///             <description><see cref="IObservationComponent"/> (HL7 FHIR R4/R5)</description>
///         </item>
///     </list>
/// </remarks>
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
