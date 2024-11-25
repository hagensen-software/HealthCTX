using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes.Interfaces;

[FhirElement]
[FhirProperty("severity", typeof(IOutcomeSeverityCode))]
[FhirProperty("code", typeof(IOutcomeCode))]
[FhirProperty("details", typeof(IOutcomeDetails))]
[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics))]
public interface IOutcomeIssue : IElement
{
}
