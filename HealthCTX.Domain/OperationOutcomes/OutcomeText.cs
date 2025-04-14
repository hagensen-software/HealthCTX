using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.OperationOutcomes;

public record OutcomeText(string Value) : ICodeableConceptText;
