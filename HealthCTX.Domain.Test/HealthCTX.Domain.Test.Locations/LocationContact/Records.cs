using HealthCTX.Domain.ExtendedContactDetails;
using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationContact;

public record ContactDetailFamilyName(string Value) : IHumanNameFamily;
public record ContactDetailName(ContactDetailFamilyName FamilyName) : IExtendedContactDetailName;
public record LocationContact(ContactDetailName? Name) : ILocationContact;

public record Location(ImmutableList<LocationContact> Contact) : ILocation;
