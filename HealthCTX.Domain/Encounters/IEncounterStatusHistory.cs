using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("status", typeof(IEncounterStatusHistoryStatus), Cardinality.Mandatory)]
[FhirProperty("period", typeof(IEncounterStatusHistoryPeriod), Cardinality.Mandatory)]
public interface IEncounterStatusHistory : IElement;
