using HealthCTX.Domain.Practitioners;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerBirthDate;

public record PractitionerBirthDate(DateOnly Value) : IPractitionerBirthDate;

public record Practitioner(PractitionerBirthDate BirthDate) : IPractitioner;
