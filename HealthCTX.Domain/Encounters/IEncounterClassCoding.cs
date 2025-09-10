using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter class.</para>
/// <para>The elements from <see cref="ICodeableConceptCoding"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IEncounterClassCoding : ICodeableConceptCoding;
