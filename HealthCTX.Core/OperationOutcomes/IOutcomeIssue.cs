using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.OperationOutcomes;

/// <summary>
/// <para>Interface for the HL7 FHIR OperationOutcome issue element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>severity</term>
///         <description><see cref="IOutcomeSeverityCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>code</term>
///         <description><see cref="IOutcomeCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>details</term>
///         <description><see cref="IOutcomeDetails"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>diagnostics</term>
///         <description><see cref="IOutcomeDiagnostics"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>expression</term>
///         <description><see cref="IOutcomeExpression"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("severity", typeof(IOutcomeSeverityCode), Cardinality.Optional)]
[FhirProperty("code", typeof(IOutcomeCode), Cardinality.Optional)]
[FhirProperty("details", typeof(IOutcomeDetails), Cardinality.Optional)]
[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics), Cardinality.Optional)]
[FhirProperty("expression", typeof(IOutcomeExpression), Cardinality.Multiple)]
public interface IOutcomeIssue : IElement;