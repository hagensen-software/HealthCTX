using HealthCTX.Domain.Availabilities;

namespace HealthCTX.Domain.PractitionerRoles;

/// <summary>
/// <para>Interface for HL7 FHIR PractitionerRole availability.</para>
/// <para>The elements from <see cref="IAvailability"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPractitionerRoleAvailability : IAvailability;
