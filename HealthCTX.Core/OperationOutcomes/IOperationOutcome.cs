using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.OperationOutcomes;

[FhirResource("OperationOutcome")]
[FhirProperty("issue", typeof(IOutcomeIssue), Cardinality.Multiple)]
public interface IOperationOutcome : IResource;
