using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;
using HealthCTX.Domain.VirtualServiceDetails;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationVirtualService;

public record ChannelTypeCode(string Value) : ICodingCode;
public record ChannelTypeSystem(Uri Value) : ICodingSystem;
public record ChannelType(ChannelTypeCode Code, ChannelTypeSystem System) : IVirtualServiceDetailChannelType;

public record AddressString(string Value) : IVirtualServiceDetailAddressString;
public record AdditionalInfo(Uri Value) : IVirtualServiceDetailAdditionalInfo;
public record MaxParticipants(uint Value) : IVirtualServiceDetailMaxParticipants;
public record SessionKey(string Value) : IVirtualServiceDetailSessionKey;

public record VirtualServiceDetail(
    ChannelType? ChannelType,
    AddressString? Address,
    AdditionalInfo? AdditionalInfo,
    MaxParticipants? MaxParticipants,
    SessionKey? SessionKey) : IVirtualServiceDetail;

public record Location(ImmutableList<VirtualServiceDetail> VirtualServiceDetail) : ILocation;