using HealthCTX.Domain.Identifiers;
using HealthCTX.Domain.Identifiers.Model;

namespace HealthCTX.Domain.Test.Model.Identifiers;

public record Identifier(
    IdentifierUse Use,
    IdentifierSystem System,
    IdentifierValue Value,
    IdentifierPeriod Period) : IIdentifier;
