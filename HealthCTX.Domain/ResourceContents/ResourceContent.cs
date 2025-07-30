using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.OperationOutcomes;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HealthCTX.Domain.ResourceContents;

public abstract record ResourceContent<TResourceContent> : IResourceContent
    where TResourceContent : ResourceContent<TResourceContent>, new()
{
    protected static JsonNode EmptyNode => new JsonObject();

    private IResource? resource;

    public static TResourceContent Create(IResource resource)
    {
        return new TResourceContent()
        {
            Value = resource
        };
    }

    public IResource Value { get => resource ?? throw new InvalidOperationException("Resource not set on ResourceContent"); /*internal*/ set => resource = value; }


    public abstract List<OutcomeIssue> ToResource(JsonElement jsonElement, string elementName, FhirVersion fhirVersion);
    public abstract (JsonNode, List<OutcomeIssue>) ToFhirJson(FhirVersion fhirVersion);

    protected static string? GetResourceType(JsonElement jsonElement)
    {
        if (jsonElement.TryGetProperty("resourceType", out JsonElement result))
            return result.GetString();
        return null;
    }

    protected static List<OutcomeIssue> ResourceTypeMissing(string elementName)
    {
        return [new OutcomeIssue(
            new OutcomeCode("invalid"),
            new OutcomeDetails(new OutcomeText($"Resource type is missing in {elementName}.")))];
    }

    protected (IResource?, List<OutcomeIssue>) ResourceTypeNotSupported(string elementName, string resourceType)
    {
        return (null, [new OutcomeIssue(
                new OutcomeCode("not-supported"),
                new OutcomeDetails(
                    new OutcomeText($"Resource type '{resourceType}' is not supported by {GetType().FullName} for {elementName}")))]);
    }
    protected (JsonNode, List<OutcomeIssue>) ResourceTypeNotSupported(IResource resource)
    {
        return (EmptyNode, [new OutcomeIssue(
                new OutcomeCode("not-supported"),
                new OutcomeDetails(
                    new OutcomeText($"Resource type '{resource.GetType().Name}' is not supported by {GetType().FullName}.")))]);
    }
}
