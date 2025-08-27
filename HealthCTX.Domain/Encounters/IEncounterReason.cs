using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("use", typeof(IEncounterReasonUse), Cardinality.Multiple)]
[FhirProperty("value", typeof(IEncounterReasonValue), Cardinality.Multiple)]
public interface IEncounterReason : IElement;
