using System.Collections.Immutable;

namespace HealthCTX.Domain.OperationOutcomes;

/// <summary>
/// Record representing an HL7 FHIR OperationOutcome resource used in generated code.
/// </summary>
public record OperationOutcome(ImmutableList<OutcomeIssue> Issues) : IOperationOutcome;
