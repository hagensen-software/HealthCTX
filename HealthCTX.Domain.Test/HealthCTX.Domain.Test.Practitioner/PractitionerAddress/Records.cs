using HealthCTX.Domain.Addresses;
using HealthCTX.Domain.Periods;
using HealthCTX.Domain.Practitioners;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerAddress;

public record AddressUse(string Value) : IAddressUse;
public record AddressType(string Value) : IAddressType;
public record AddressText(string Value) : IAddressText;
public record AddressLine(string Value) : IAddressLine;
public record AddressCity(string Value) : IAddressCity;
public record AddressDistrict(string Value) : IAddressDistrict;
public record AddressState(string Value) : IAddressState;
public record AddressPostalCode(string Value) : IAddressPostalCode;
public record AddressCountry(string Value) : IAddressCountry;
public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
public record AddressPeriod(PeriodStart Start, PeriodEnd End) : IAddressPeriod;

public record PractitionerAddress(
    AddressUse Use,
    AddressType Type,
    AddressText Text,
    ImmutableList<AddressLine> Lines,
    AddressCity City,
    AddressDistrict District,
    AddressState State,
    AddressPostalCode PostalCode,
    AddressCountry Country,
    AddressPeriod Period) : IPractitionerAddress;

public record Practitioner(ImmutableList<PractitionerAddress> Address) : IPractitioner;
