using HealthCTX.Domain.Availabilities;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for the HL7 FHIR Location hoursOfOperation element (R6).</para>
/// <para>The elements from <see cref="IAvailability"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ILocationHoursOfOperationR6 : IAvailability;
