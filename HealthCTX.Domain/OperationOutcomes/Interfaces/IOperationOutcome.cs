using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.OperationOutcomes.Interfaces;

[FhirResource("OperationOutcome")]
[FhirProperty("issue", typeof(IOutcomeIssue))]
public interface IOperationOutcome : IResource;
