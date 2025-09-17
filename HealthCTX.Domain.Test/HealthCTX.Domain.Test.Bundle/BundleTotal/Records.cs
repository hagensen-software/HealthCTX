using HealthCTX.Domain.Bundles;

namespace HealthCTX.Domain.Test.Bundle.BundleTotal;

public record BundleType(string Value) : IBundleType;

public record Total(uint Value) : IBundleTotal;

public record Bundle(BundleType Type, Total? Total) : IBundle;
