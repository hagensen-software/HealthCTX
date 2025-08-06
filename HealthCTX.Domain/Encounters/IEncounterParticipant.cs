using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("type", typeof(IEncounterParticipantType), Cardinality.Multiple)]
[FhirProperty("period", typeof(IEncounterParticipantPeriod), Cardinality.Optional)]
[FhirProperty("individual", typeof(IEncounterParticipantIndividual), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("actor", typeof(IEncounterParticipantActor), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IEncounterParticipant : IElement;
