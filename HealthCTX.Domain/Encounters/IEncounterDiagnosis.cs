using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("condition", typeof(IEncounterDiagnosisConditionReference), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("condition", typeof(IEncounterDiagnosisCondition), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("use", typeof(IEncounterDiagnosisUse), Cardinality.Optional)]
[FhirProperty("rank", typeof(IEncounterDiagnosisRank), Cardinality.Optional, ToVersion: FhirVersion.R4)]
public interface IEncounterDiagnosis : IElement;
