using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.OperationOutcomes;

[FhirElement]
[FhirProperty("severity", typeof(IOutcomeSeverityCode), Cardinality.Optional)]
[FhirProperty("code", typeof(IOutcomeCode), Cardinality.Optional)]
[FhirProperty("details", typeof(IOutcomeDetails), Cardinality.Optional)]
[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics), Cardinality.Optional)]
[FhirProperty("expression", typeof(IOutcomeExpression), Cardinality.Multiple)]
public interface IOutcomeIssue : IElement;