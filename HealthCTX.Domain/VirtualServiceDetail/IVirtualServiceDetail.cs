using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.VirtualServiceDetail;

/// <summary>
/// <para>Interface for the HL7 FHIR VirtualServiceDetail element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>channelType</term>
///         <description><see cref="IVirtualServiceDetailChannelType"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address[Url]</term>
///         <description><see cref="IVirtualServiceDetailAddressUrl"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address[String]</term>
///         <description><see cref="IVirtualServiceDetailAddressString"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address[ContactPoint]</term>
///         <description><see cref="IVirtualServiceDetailAddressContactPoint"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>address[ExtendedContactDetail]</term>
///         <description><see cref="IVirtualServiceDetailAddressExtendedContactDetail"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>additionalInfo</term>
///         <description><see cref="IVirtualServiceDetailAdditionalInfo"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>maxParticipants</term>
///         <description><see cref="IVirtualServiceDetailMaxParticipants"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>sessionKey</term>
///         <description><see cref="IVirtualServiceDetailSessionKey"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement()]
[FhirProperty("channelType", typeof(IVirtualServiceDetailChannelType), Cardinality.Optional)]
[FhirProperty("address[Url]", typeof(IVirtualServiceDetailAddressUrl), Cardinality.Optional)]
[FhirProperty("address[String]", typeof(IVirtualServiceDetailAddressString), Cardinality.Optional)]
[FhirProperty("address[ContactPoint]", typeof(IVirtualServiceDetailAddressContactPoint), Cardinality.Optional)]
[FhirProperty("address[ExtendedContactDetail]", typeof(IVirtualServiceDetailAddressExtendedContactDetail), Cardinality.Optional)]
[FhirProperty("additionalInfo", typeof(IVirtualServiceDetailAdditionalInfo), Cardinality.Optional)]
[FhirProperty("maxParticipants", typeof(IVirtualServiceDetailMaxParticipants), Cardinality.Optional)]
[FhirProperty("sessionKey", typeof(IVirtualServiceDetailSessionKey), Cardinality.Optional)]
public interface IVirtualServiceDetail : IElement;
