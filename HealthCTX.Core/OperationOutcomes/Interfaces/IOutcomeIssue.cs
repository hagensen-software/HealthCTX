using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes.Interfaces;

// TODO : Add expression property
[FhirElement]
[FhirProperty("severity", typeof(IOutcomeSeverityCode), Cardinality.Optional)]
[FhirProperty("code", typeof(IOutcomeCode), Cardinality.Optional)]
[FhirProperty("details", typeof(IOutcomeDetails), Cardinality.Optional)]
[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics), Cardinality.Optional)]
public interface IOutcomeIssue : IElement;