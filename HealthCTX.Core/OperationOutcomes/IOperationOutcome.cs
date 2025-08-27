using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.OperationOutcomes;

/// <summary>
/// <para>Interface for the HL7 FHIR Patient resource.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>issue</term>
///         <description><see cref="IOutcomeIssue"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirResource("OperationOutcome")]
[FhirProperty("issue", typeof(IOutcomeIssue), Cardinality.Multiple)]
public interface IOperationOutcome : IResource;
