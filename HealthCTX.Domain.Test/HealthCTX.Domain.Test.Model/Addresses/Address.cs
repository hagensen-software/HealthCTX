using HealthCTX.Domain.Addresses;
using HealthCTX.Domain.Addresses.Model;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Model.Addresses;

public record Address(
    ImmutableList<AddressLine> Lines,
    AddressPostalCode? PostalCode,
    AddressCity? City,
    AddressDistrict? District,
    AddressCountry? Country,
    AddressState? State,
    AddressType? Type,
    AddressUse? Use,
    AddressText? Text,
    AddressPeriod? Period
    ) : IAddress;
