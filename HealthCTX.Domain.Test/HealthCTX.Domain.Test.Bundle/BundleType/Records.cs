using HealthCTX.Domain.Bundle;

namespace HealthCTX.Domain.Test.Bundle.BundleType;

public record BundleType(string Value) : IBundleType;

public record Bundle(BundleType Type) : IBundle;
