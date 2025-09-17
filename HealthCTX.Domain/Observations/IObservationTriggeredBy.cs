using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for the HL7 FHIR Observation triggeredBy element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>observation</term>
///         <description><see cref="IObservationTriggeredByObservation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IObservationTriggeredByType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>reason</term>
///         <description><see cref="IObservationTriggeredByReason"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("observation", typeof(IObservationTriggeredByObservation), Cardinality.Mandatory)]
[FhirProperty("type", typeof(IObservationTriggeredByType), Cardinality.Mandatory)]
[FhirProperty("reason", typeof(IObservationTriggeredByReason), Cardinality.Optional)]
public interface IObservationTriggeredBy : IElement;
