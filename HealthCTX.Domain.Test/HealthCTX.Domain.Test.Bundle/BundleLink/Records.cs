using HealthCTX.Domain.Bundle;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Bundle.BundleLink;

public record BundleType(string Value) : IBundleType;

public record BundleLinkRelation(string Value) : IBundleLinkRelation;
public record BundleLinkUrl(Uri Value) : IBundleLinkUrl;
public record BundleLink(BundleLinkRelation Relation, BundleLinkUrl Url) : IBundleLink;

public record Bundle(BundleType Type, ImmutableList<BundleLink> Links) : IBundle;
