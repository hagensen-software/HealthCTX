using HealthCTX.Domain.Bundles;

namespace HealthCTX.Domain.Test.Bundle.BundleTimestamp;

public record BundleType(string Value) : IBundleType;

public record Timestamp(DateTimeOffset Value) : IBundleTimestamp;

public record Bundle(BundleType Type, Timestamp? Timestamp) : IBundle;
