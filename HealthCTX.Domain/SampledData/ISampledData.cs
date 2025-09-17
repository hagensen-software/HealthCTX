using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.SampledData;

/// <summary>
/// <para>Interface for the HL7 FHIR SampledData element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>origin</term>
///         <description><see cref="ISampledDataOrigin"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="ISampledDataPeriod"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>interval</term>
///         <description><see cref="ISampledDataInterval"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>intervalUnit</term>
///         <description><see cref="ISampledDataIntervalUnit"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>factor</term>
///         <description><see cref="ISampledDataFactor"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>lowerLimit</term>
///         <description><see cref="ISampledDataLowerLimit"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>upperLimit</term>
///         <description><see cref="ISampledDataUpperLimit"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>dimensions</term>
///         <description><see cref="ISampledDataDimensions"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>codeMap</term>
///         <description><see cref="ISampledDataCodeMap"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>offsets</term>
///         <description><see cref="ISampledDataOffsets"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>data</term>
///         <description><see cref="ISampledDataData"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("origin", typeof(ISampledDataOrigin), Cardinality.Mandatory)]
[FhirProperty("period", typeof(ISampledDataPeriod), Cardinality.Mandatory, ToVersion: FhirVersion.R4)]
[FhirProperty("interval", typeof(ISampledDataInterval), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("intervalUnit", typeof(ISampledDataIntervalUnit), Cardinality.Mandatory, FromVersion: FhirVersion.R5)]
[FhirProperty("factor", typeof(ISampledDataFactor), Cardinality.Optional)]
[FhirProperty("lowerLimit", typeof(ISampledDataLowerLimit), Cardinality.Optional)]
[FhirProperty("upperLimit", typeof(ISampledDataUpperLimit), Cardinality.Optional)]
[FhirProperty("dimensions", typeof(ISampledDataDimensions), Cardinality.Mandatory)]
[FhirProperty("codeMap", typeof(ISampledDataCodeMap), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("offsets", typeof(ISampledDataOffsets), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("data", typeof(ISampledDataData), Cardinality.Optional)]
public interface ISampledData : IElement;
