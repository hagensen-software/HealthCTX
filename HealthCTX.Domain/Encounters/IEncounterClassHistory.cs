using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("class", typeof(IEncounterClassHistoryClass), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IEncounterClassHistoryPeriod), Cardinality.Mandatory)]
public interface IEncounterClassHistory : IElement;
