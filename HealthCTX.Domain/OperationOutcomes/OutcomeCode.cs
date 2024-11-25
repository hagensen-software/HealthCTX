using HealthCTX.Domain.OperationOutcomes.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes;

public record OutcomeCode(string Value) : IOutcomeCode;
