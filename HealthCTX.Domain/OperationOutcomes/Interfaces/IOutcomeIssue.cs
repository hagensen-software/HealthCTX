using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes.Interfaces;

[FhirElement]
[FhirProperty("severity", typeof(IOutcomeSeverityCode), Cardinality.Single)]
[FhirProperty("code", typeof(IOutcomeCode), Cardinality.Single)]
[FhirProperty("details", typeof(IOutcomeDetails), Cardinality.Single)]
[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics), Cardinality.Single)]
public interface IOutcomeIssue : IElement
{
}
