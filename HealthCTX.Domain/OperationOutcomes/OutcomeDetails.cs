using HealthCTX.Domain.OperationOutcomes.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes;

public record OutcomeDetails(OutcomeText Text) : IOutcomeDetails;
