using HealthCTX.Domain.Identifiers;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter identifier.</para>
/// <para>The elements from <see cref="IIdentifier"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IEncounterIdentifier : IIdentifier;
