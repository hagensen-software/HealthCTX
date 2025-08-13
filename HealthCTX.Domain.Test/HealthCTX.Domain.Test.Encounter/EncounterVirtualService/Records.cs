using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Encounters;
using HealthCTX.Domain.VirtualServiceDetail;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Encounter.EncounterVirtualService;

public record Status(string Value) : IEncounterStatus;

public record ChannelTypeCode(string Value) : ICodingCode;
public record ChannelTypeSystem(Uri Value) : ICodingSystem;
public record ChannelType(ChannelTypeCode Code, ChannelTypeSystem System) : IVirtualServiceDetailChannelType;

public record AddressString(string Value) : IVirtualServiceDetailAddressString;

public record EncounterVirtualService(
    ChannelType? ChannelType,
    AddressString? Address) : IEncounterVirtualService;

public record Encounter(Status Status, ImmutableList<EncounterVirtualService> VirtualServices) : IEncounter;
