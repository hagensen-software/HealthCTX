using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Bundle;
using HealthCTX.Domain.OperationOutcomes;
using HealthCTX.Domain.Patients;
using HealthCTX.Domain.ResourceContents;
using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HealthCTX.Domain.Test.Bundle.BundleEntry;

public record PatientId(string Value) : IId;
public record Patient(PatientId Id) : IPatient;

public record BundleType(string Value) : IBundleType;

public record EntryLinkRelation(string Value) : IBundleLinkRelation;
public record EntryLinkUrl(Uri Value) : IBundleLinkUrl;
public record EntryLink(EntryLinkRelation Relation, EntryLinkUrl Url) : IBundleLink;

public record EntryFullUrl(Uri Value) : IBundleEntryFullUrl;
public record EntryResource : ResourceContent<EntryResource>, IBundleEntryResource
{
    public override (JsonNode, List<OutcomeIssue>) ToFhirJson(FhirVersion fhirVersion)
    {
        return Value switch
        {
            Patient p => p.ToFhirJson(fhirVersion),
            _ => ResourceTypeNotSupported(Value)
        };
    }

    public override List<OutcomeIssue> ToResource(JsonElement jsonElement, string elementName, FhirVersion fhirVersion)
    {
        string? resourceType = GetResourceType(jsonElement);
        if (resourceType is null)
            return ResourceTypeMissing(elementName);

        (var resource, var outcomes) = resourceType switch
        {
            "Patient" => PatientFhirJsonMapper.ToPatient(jsonElement, elementName, fhirVersion),
            _ => ResourceTypeNotSupported(elementName, resourceType)
        };

        if (resource is not null)
            Value = resource;

        return outcomes;
    }
}

public record EntrySearchMode(string Value) : IBundleEntrySearchMode;
public record EntrySearchScore(double Value) : IBundleEntrySearchScore;
public record EntrySearch(EntrySearchMode? Mode, EntrySearchScore? Score) : IBundleEntrySearch;

public record EntryRequestMethod(string Value) : IBundleEntryRequestMethod;
public record EntryRequestUrl(Uri Value) : IBundleEntryRequestUrl;
public record EntryRequestIfNoneMatch(string Value) : IBundleEntryRequestIfNoneMatch;
public record EntryRequestIfModifiedSince(DateTimeOffset Value) : IBundleEntryRequestIfModifiedSince;
public record EntryRequestIfMatch(string Value) : IBundleEntryRequestIfMatch;
public record EntryRequestIfNoneExist(string Value) : IBundleEntryRequestIfNoneExist;
public record EntryRequest(
    EntryRequestMethod Method,
    EntryRequestUrl Url,
    EntryRequestIfNoneMatch? IfNoneMatch,
    EntryRequestIfModifiedSince? IfModifiedSince,
    EntryRequestIfMatch? IfMatch,
    EntryRequestIfNoneExist? IfNoneExist) : IBundleEntryRequest;

public record EntryResponseStatus(string Value) : IBundleEntryResponseStatus;
public record EntryResponseLocation(Uri Value) : IBundleEntryResponseLocation;
public record EntryResponseEtag(string Value) : IBundleEntryResponseEtag;
public record EntryResponseLastModified(DateTimeOffset Value) : IBundleEntryResponseLastModified;
public record EntryResponseOutcome : ResourceContent<EntryResponseOutcome>, IBundleEntryResponseOutcome
{
    public override (JsonNode, List<OutcomeIssue>) ToFhirJson(FhirVersion fhirVersion)
    {
        return Value switch
        {
            OperationOutcome o => o.ToFhirJson(fhirVersion),
            _ => ResourceTypeNotSupported(Value)
        };
    }

    public override List<OutcomeIssue> ToResource(JsonElement jsonElement, string elementName, FhirVersion fhirVersion)
    {
        string? resourceType = GetResourceType(jsonElement);
        if (resourceType is null)
            return ResourceTypeMissing(elementName);

        (var resource, var outcomes) = resourceType switch
        {
            "OperationOutcome" => OperationOutcomeFhirJsonMapper.ToOperationOutcome(jsonElement, elementName, fhirVersion),
            _ => ResourceTypeNotSupported(elementName, resourceType)
        };

        if (resource is not null)
            Value = resource;

        return outcomes;
    }
}

public record EntryResponse(
    EntryResponseStatus Status,
    EntryResponseLocation Location,
    EntryResponseEtag? Etag,
    EntryResponseLastModified? LastModified,
    EntryResponseOutcome? Outcome) : IBundleEntryResponse;


public record BundleEntry(
    ImmutableList<EntryLink> Links,
    EntryFullUrl FullUrl,
    EntryResource? Resource,
    EntrySearch? Search,
    EntryRequest? Request,
    EntryResponse? Response) : IBundleEntry;

public record Bundle(BundleType Type, ImmutableList<BundleEntry> Entries) : IBundle;
