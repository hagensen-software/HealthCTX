using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Availabilities;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for the HL7 FHIR Location hoursOfOperation element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>availableTime</term>
///         <description><see cref="IAvailabilityAvailable"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>notAvailableTime</term>
///         <description><see cref="IAvailabilityNotAvailable"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>daysOfWeek</term>
///         <description><see cref="IAvailabilityAvailableDaysOfWeek"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>allDay</term>
///         <description><see cref="IAvailabilityAvailableAllDay"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>openingTime</term>
///         <description><see cref="ILocationHoursOfOperationOpeningTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>closingTime</term>
///         <description><see cref="ILocationHoursOfOperationClosingTime"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("availableTime", typeof(IAvailabilityAvailable), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("notAvailableTime", typeof(IAvailabilityNotAvailable), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("daysOfWeek", typeof(IAvailabilityAvailableDaysOfWeek), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("allDay", typeof(IAvailabilityAvailableAllDay), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("openingTime", typeof(ILocationHoursOfOperationOpeningTime), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("closingTime", typeof(ILocationHoursOfOperationClosingTime), Cardinality.Optional, ToVersion: FhirVersion.R4)]
public interface ILocationHoursOfOperation : IElement;
