using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for the HL7 FHIR Observation referenceRange element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>low</term>
///         <description><see cref="IObservationReferenceRangeLow"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>high</term>
///         <description><see cref="IObservationReferenceRangeHigh"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>normalValue</term>
///         <description><see cref="IObservationReferenceRangeNormalValue"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IObservationReferenceRangeType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>appliesTo</term>
///         <description><see cref="IObservationReferenceRangeAppliesTo"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>age</term>
///         <description><see cref="IObservationReferenceRangeAge"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("low", typeof(IObservationReferenceRangeLow), Cardinality.Optional)]
[FhirProperty("high", typeof(IObservationReferenceRangeHigh), Cardinality.Optional)]
[FhirProperty("normalValue", typeof(IObservationReferenceRangeNormalValue), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("type", typeof(IObservationReferenceRangeType), Cardinality.Optional)]
[FhirProperty("appliesTo", typeof(IObservationReferenceRangeAppliesTo), Cardinality.Multiple)]
[FhirProperty("age", typeof(IObservationReferenceRangeAge), Cardinality.Optional)]
public interface IObservationReferenceRange : IElement;
