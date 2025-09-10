using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Availability;

/// <summary>
/// <para>Interface for the HL7 FHIR Location hoursOfOperation and Availability element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>daysOfWeek</term>
///         <description><see cref="IAvailabilityAvailableDaysOfWeek"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>allDay</term>
///         <description><see cref="IAvailabilityAvailableAllDay"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>availableStartTime</term>
///         <description><see cref="IAvailabilityAvailableStartTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>availableEndTime</term>
///         <description><see cref="IAvailabilityAvailableEndTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("daysOfWeek", typeof(IAvailabilityAvailableDaysOfWeek), Cardinality.Multiple)]
[FhirProperty("allDay", typeof(IAvailabilityAvailableAllDay), Cardinality.Optional)]
[FhirProperty("availableStartTime", typeof(IAvailabilityAvailableStartTime), Cardinality.Optional)]
[FhirProperty("availableEndTime", typeof(IAvailabilityAvailableEndTime), Cardinality.Optional)]
public interface IAvailabilityAvailable : IElement;
