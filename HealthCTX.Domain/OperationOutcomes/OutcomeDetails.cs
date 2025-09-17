namespace HealthCTX.Domain.OperationOutcomes;

/// <summary>
/// Record representing an HL7 FHIR OperationOutcome text used in generated code.
/// </summary>
public record OutcomeDetails(OutcomeText Text) : IOutcomeDetails;
