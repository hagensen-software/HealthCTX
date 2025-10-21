using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Extensions;

namespace HealthCTX.Domain;

/// <summary>
/// <para>Interface for all HL7 FHIR elements.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>id</term>
///         <description><see cref="IId"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>extension</term>
///         <description><see cref="IExtension"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirProperty("id", typeof(IId), Cardinality.Optional)]
[FhirProperty("extension", typeof(IExtension), Cardinality.Multiple)]
public interface IElement;
