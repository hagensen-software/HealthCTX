using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter statusHistory.status.</para>
/// <para>The primitive element <see cref="ICodingCode"/> is supported.</para>
/// </summary>
public interface IEncounterStatusHistoryStatus : ICodingCode;
