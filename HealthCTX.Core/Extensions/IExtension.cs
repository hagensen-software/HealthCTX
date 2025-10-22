using HealthCTX.Domain.Addresses;
using HealthCTX.Domain.Annotations;
using HealthCTX.Domain.Attachments;
using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Availabilities;
using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.CodeableReferences;
using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.ExtendedContactDetails;
using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Periods;
using HealthCTX.Domain.Quantities;
using HealthCTX.Domain.Ranges;
using HealthCTX.Domain.Ratios;
using HealthCTX.Domain.References;
using HealthCTX.Domain.SampledData;
using HealthCTX.Domain.Signatures;
using HealthCTX.Domain.Timings;

namespace HealthCTX.Domain.Extensions;

/// <summary>
/// <para>Interface for the HL7 FHIR Extension element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>url</term>
///         <description><see cref="IExtensionUri"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>value[x]</term>
///         <description>For a complete list of value types, most of which is supported, see <see href="https://hl7.org/fhir/R4/extensibility.html#Extension"/> and <see href="https://hl7.org/fhir/R5/extensibility.html#Extension"/> for HL7 FHIR R4 and R5 respectively. Navigate to the interface to see which ones are supported.</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("url", typeof(IExtensionUri), Cardinality.Mandatory)]
[FhirProperty("value[Base64Binary]", typeof(IBase64BinaryPrimitive), Cardinality.Optional)]
[FhirProperty("value[Boolean]", typeof(IBooleanPrimitive), Cardinality.Optional)]
[FhirProperty("value[Canonical]", typeof(ICanonicalPrimitive), Cardinality.Optional)]
[FhirProperty("value[Code]", typeof(ICodingCode), Cardinality.Optional)]
[FhirProperty("value[Date]", typeof(IDatePrimitive), Cardinality.Optional)]
[FhirProperty("value[DateTime]", typeof(IDateTimePrimitive), Cardinality.Optional)]
[FhirProperty("value[Decimal]", typeof(IDecimalPrimitive), Cardinality.Optional)]
//[FhirProperty("value[Id]", typeof(IId), Cardinality.Optional)]
[FhirProperty("value[Instant]", typeof(IInstantPrimitive), Cardinality.Optional)]
[FhirProperty("value[Integer]", typeof(IIntegerPrimitive), Cardinality.Optional)]
[FhirProperty("value[Integer64]", typeof(IInteger64Primitive), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("value[Markdown]", typeof(IMarkdownPrimitive), Cardinality.Optional)]
[FhirProperty("value[Oid]", typeof(IOidPrimitive), Cardinality.Optional)]
[FhirProperty("value[PositiveInt]", typeof(IPositiveIntegerPrimitive), Cardinality.Optional)]
[FhirProperty("value[String]", typeof(IStringPrimitive), Cardinality.Optional)]
[FhirProperty("value[Time]", typeof(ITimePrimitive), Cardinality.Optional)]
[FhirProperty("value[UnsignedInt]", typeof(IUnsignedIntegerPrimitive), Cardinality.Optional)]
[FhirProperty("value[Uri]", typeof(IUriPrimitive), Cardinality.Optional)]
[FhirProperty("value[Url]", typeof(IUrlPrimitive), Cardinality.Optional)]
[FhirProperty("value[Uuid]", typeof(IUuidPrimitive), Cardinality.Optional)]
[FhirProperty("value[Address]", typeof(IAddress), Cardinality.Optional)]
[FhirProperty("value[Age]", typeof(IAge), Cardinality.Optional)]
[FhirProperty("value[Annotation]", typeof(IAnnotation), Cardinality.Optional)]
[FhirProperty("value[Attachment]", typeof(IAttachment), Cardinality.Optional)]
[FhirProperty("value[CodeableConcept]", typeof(ICodeableConcept), Cardinality.Optional)]
[FhirProperty("value[CodeableReference]", typeof(ICodeableReference), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("value[Coding]", typeof(ICodeableConceptCoding), Cardinality.Optional)]
[FhirProperty("value[ContactPoint]", typeof(IContactPoint), Cardinality.Optional)]
[FhirProperty("value[Count]", typeof(ICount), Cardinality.Optional)]
[FhirProperty("value[Distance]", typeof(IDistance), Cardinality.Optional)]
[FhirProperty("value[Duration]", typeof(IDuration), Cardinality.Optional)]
[FhirProperty("value[HumanName]", typeof(IHumanName), Cardinality.Optional)]
[FhirProperty("value[Identifier]", typeof(IIdentifier), Cardinality.Optional)]
[FhirProperty("value[Money]", typeof(IMoney), Cardinality.Optional)]
[FhirProperty("value[Period]", typeof(IPeriod), Cardinality.Optional)]
[FhirProperty("value[Quantity]", typeof(IQuantity), Cardinality.Optional)]
[FhirProperty("value[Range]", typeof(IRange), Cardinality.Optional)]
[FhirProperty("value[Ratio]", typeof(IRatio), Cardinality.Optional)]
//[FhirProperty("value[RatioRange]", typeof(IRatioRange), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("value[Reference]", typeof(IReference), Cardinality.Optional)]
[FhirProperty("value[SampledData]", typeof(ISampledData), Cardinality.Optional)]
[FhirProperty("value[Signature]", typeof(ISignature), Cardinality.Optional)]
[FhirProperty("value[Timing]", typeof(ITiming), Cardinality.Optional)]
//[FhirProperty("value[ContactDetail]", typeof(IContactDetail), Cardinality.Optional)]
//[FhirProperty("value[Contributor]", typeof(IContributor), Cardinality.Optional, ToVersion: FhirVersion.R4)]
//[FhirProperty("value[DataRequirement]", typeof(IDataRequirement), Cardinality.Optional)]
//[FhirProperty("value[Expression]", typeof(IExpression), Cardinality.Optional)]
//[FhirProperty("value[ParameterDefinition]", typeof(IParameterDefinition), Cardinality.Optional)]
//[FhirProperty("value[RelatedArtifact]", typeof(IRelatedArtifact), Cardinality.Optional)]
//[FhirProperty("value[TriggerDefinition]", typeof(ITriggerDefinition), Cardinality.Optional)]
//[FhirProperty("value[UsageContext]", typeof(IUsageContext), Cardinality.Optional)]
[FhirProperty("value[Availability]", typeof(IAvailability), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("value[ExtendedContactDetail]", typeof(IExtendedContactDetail), Cardinality.Optional, FromVersion: FhirVersion.R5)]
//[FhirProperty("value[Dosage]", typeof(IDosage), Cardinality.Optional)]
//[FhirProperty("value[Meta]", typeof(IMeta), Cardinality.Optional)]
public interface IExtension : IElement;
