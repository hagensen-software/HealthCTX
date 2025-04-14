using HealthCTX.Domain.Practitioners;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerGender;

public record PractitionerGender(string Value) : IPractitionerGender;

public record Practitioner(PractitionerGender Gender) : IPractitioner;