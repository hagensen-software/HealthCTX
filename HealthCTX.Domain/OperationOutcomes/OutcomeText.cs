using HealthCTX.Domain.CodeableConcepts.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes;

public record OutcomeText(string Value) : ICodeableConceptText;
