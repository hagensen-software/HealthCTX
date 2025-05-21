using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.VirtualServiceDetail;

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
