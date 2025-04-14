using HealthCTX.Domain.HumanName;
using HealthCTX.Domain.Practitioner;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerName;

public record PractitionerFamilyName(string Value) : IHumanNameFamily;
public record PractitionerGivenName(string Value) : IHumanNameGiven;

public record PractitionerName(PractitionerFamilyName Family, ImmutableList<PractitionerGivenName> Given) : IPractitionerHumanName;

public record Practitioner(PractitionerName Name) : IPractitioner;