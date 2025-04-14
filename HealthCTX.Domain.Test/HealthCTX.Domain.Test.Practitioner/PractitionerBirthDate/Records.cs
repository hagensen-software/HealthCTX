using HealthCTX.Domain.Practitioner;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerBirthDate;

public record PractitionerBirthDate(DateOnly Value) : IPractitionerBirthDate;

public record Practitioner(PractitionerBirthDate BirthDate) : IPractitioner;
