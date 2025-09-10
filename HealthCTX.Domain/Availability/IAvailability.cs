using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Availability;

/// <summary>
/// <para>Interface for the HL7 FHIR Availability element.</para>
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
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("availableTime", typeof(IAvailabilityAvailable), Cardinality.Multiple)]
[FhirProperty("notAvailableTime", typeof(IAvailabilityNotAvailable), Cardinality.Multiple)]
public interface IAvailability : IElement;
