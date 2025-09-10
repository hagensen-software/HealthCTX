using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter statusHistory.status.</para>
/// <para>The elements from <see cref="ICodingCode"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IEncounterStatusHistoryStatus : ICodingCode;
