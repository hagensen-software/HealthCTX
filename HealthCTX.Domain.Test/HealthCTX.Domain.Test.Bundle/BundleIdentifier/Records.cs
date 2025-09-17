using HealthCTX.Domain.Bundles;
using HealthCTX.Domain.Identifiers;

namespace HealthCTX.Domain.Test.Bundle.BundleIdentifier;

public record BundleType(string Value) : IBundleType;

public record IdentifierSystem(Uri Value) : IIdentifierSystem;
public record IdentifierValue(string Value) : IIdentifierValue;
public record Identifier(IdentifierSystem? System, IdentifierValue? Value) : IBundleIdentifier;

public record Bundle(BundleType Type, Identifier? Identifier) : IBundle;