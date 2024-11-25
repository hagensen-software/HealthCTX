using HealthCTX.Domain.OperationOutcomes.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes;

public record OutcomeIssue(OutcomeCode Code, OutcomeDetails Details) : IOutcomeIssue
{
    public static OutcomeIssue CreateStructureError(string details) => new(new OutcomeCode("structure"), new OutcomeDetails(new OutcomeText(details)));
    public static OutcomeIssue CreateValueError(string details) => new(new OutcomeCode("value"), new OutcomeDetails(new OutcomeText(details)));
    public static OutcomeIssue CreateRequiredError(string details) => new(new OutcomeCode("required"), new OutcomeDetails(new OutcomeText(details))); 
}
