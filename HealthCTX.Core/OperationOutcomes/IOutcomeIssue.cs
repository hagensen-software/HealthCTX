using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.OperationOutcomes;

// TODO : Add expression property
[FhirElement]
[FhirProperty("severity", typeof(IOutcomeSeverityCode), Cardinality.Optional)]
[FhirProperty("code", typeof(IOutcomeCode), Cardinality.Optional)]
[FhirProperty("details", typeof(IOutcomeDetails), Cardinality.Optional)]
[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics), Cardinality.Optional)]
public interface IOutcomeIssue : IElement;