using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter admission.destination and hospitalization.destination.</para>
/// <para>The elements from <see cref="IReference"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IEncounterAdmissionDestination : IReference;
