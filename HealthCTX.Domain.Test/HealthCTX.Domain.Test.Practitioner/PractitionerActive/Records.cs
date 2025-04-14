using HealthCTX.Domain.Practitioner;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerActive;

public record PractitionerActive(bool Value) : IPractitionerActive;
public record Practitioner(PractitionerActive Active) : IPractitioner;