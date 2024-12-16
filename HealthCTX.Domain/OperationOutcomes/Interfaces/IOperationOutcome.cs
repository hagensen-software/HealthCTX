using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes.Interfaces;

[FhirResource("OperationOutcome")]
[FhirProperty("issue", typeof(IOutcomeIssue), Cardinality.Multiple)]
public interface IOperationOutcome : IResource;
