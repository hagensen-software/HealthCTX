using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Addresses;

/// <summary>
/// <para>Interface for the HL7 FHIR Identifier element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>use</term>
///         <description><see cref="IAddressUse"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>type</term>
///         <description><see cref="IAddressType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>text</term>
///         <description><see cref="IAddressText"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>line</term>
///         <description><see cref="IAddressLine"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>city</term>
///         <description><see cref="IAddressCity"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>district</term>
///         <description><see cref="IAddressDistrict"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>state</term>
///         <description><see cref="IAddressState"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>postalCode</term>
///         <description><see cref="IAddressPostalCode"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>country</term>
///         <description><see cref="IAddressCountry"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IAddressPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("use", typeof(IAddressUse), Cardinality.Optional)]
[FhirProperty("type", typeof(IAddressType), Cardinality.Optional)]
[FhirProperty("text", typeof(IAddressText), Cardinality.Optional)]
[FhirProperty("line", typeof(IAddressLine), Cardinality.Multiple)]
[FhirProperty("city", typeof(IAddressCity), Cardinality.Optional)]
[FhirProperty("district", typeof(IAddressDistrict), Cardinality.Optional)]
[FhirProperty("state", typeof(IAddressState), Cardinality.Optional)]
[FhirProperty("postalCode", typeof(IAddressPostalCode), Cardinality.Optional)]
[FhirProperty("country", typeof(IAddressCountry), Cardinality.Optional)]
[FhirProperty("period", typeof(IAddressPeriod), Cardinality.Optional)]
public interface IAddress : IElement;
