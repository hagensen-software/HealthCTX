using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.OperationOutcomes;

/// <summary>
/// Record representing an HL7 FHIR OperationOutcome text used in generated code.
/// </summary>
public record OutcomeText(string Value) : ICodeableConceptText;
