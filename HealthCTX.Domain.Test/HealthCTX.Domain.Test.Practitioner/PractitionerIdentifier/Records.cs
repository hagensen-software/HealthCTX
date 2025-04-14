using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Practitioner;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerIdentifier;

public record PractitionerIdentifierSystem(Uri Value) : IIdentifierSystem;
public record PractitionerIdentifierValue(string Value) : IIdentifierValue;
public record PractitionerIdentifier(PractitionerIdentifierSystem System, PractitionerIdentifierValue Value) : IPractitionerIdentifier;

public record Practitioner(PractitionerIdentifier Identifier) : IPractitioner;
