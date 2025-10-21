using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.HumanNames;

/// <summary>
/// <para>Interface for the HL7 FHIR HumanName element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>use</term>
///         <description><see cref="IHumanNameUse"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>text</term>
///         <description><see cref="IHumanNameText"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>family</term>
///         <description><see cref="IHumanNameFamily"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>given</term>
///         <description><see cref="IHumanNameGiven"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>prefix</term>
///         <description><see cref="IHumanNamePrefix"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>suffix</term>
///         <description><see cref="IHumanNameSuffix"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IHumanNamePeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("use", typeof(IHumanNameUse), Cardinality.Optional)]
[FhirProperty("text", typeof(IHumanNameText), Cardinality.Optional)]
[FhirProperty("family", typeof(IHumanNameFamily), Cardinality.Optional)]
[FhirProperty("given", typeof(IHumanNameGiven), Cardinality.Multiple)]
[FhirProperty("prefix", typeof(IHumanNamePrefix), Cardinality.Multiple)]
[FhirProperty("suffix", typeof(IHumanNameSuffix), Cardinality.Multiple)]
[FhirProperty("period", typeof(IHumanNamePeriod), Cardinality.Optional)]
public interface IHumanName : IElement;
