namespace HealthCTX.Domain.OperationOutcomes;

/// <summary>
/// Record representing an HL7 FHIR OperationOutcome issue used in generated code.
/// </summary>
public record OutcomeIssue(OutcomeCode Code, OutcomeDetails Details) : IOutcomeIssue
{
    /// <summary>
    /// Helper constructing an OutcomeIssue with a structure error code and details.
    /// </summary>
    public static OutcomeIssue CreateStructureError(string details) => new(new OutcomeCode("structure"), new OutcomeDetails(new OutcomeText(details)));
    /// <summary>
    /// Helper constructing an OutcomeIssue with a value error code and details.
    /// </summary>
    public static OutcomeIssue CreateValueError(string details) => new(new OutcomeCode("value"), new OutcomeDetails(new OutcomeText(details)));
    /// <summary>
    /// Helper constructing an OutcomeIssue with a required error code and details.
    /// </summary>
    public static OutcomeIssue CreateRequiredError(string details) => new(new OutcomeCode("required"), new OutcomeDetails(new OutcomeText(details))); 
}
