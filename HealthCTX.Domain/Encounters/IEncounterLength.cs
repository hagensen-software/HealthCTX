using HealthCTX.Domain.Quantity;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter length.</para>
/// <para>The elements from <see cref="IDuration"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IEncounterLength : IDuration;
