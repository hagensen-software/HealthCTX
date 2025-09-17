using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.OperationOutcomes;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace HealthCTX.Domain.ResourceContents;

/// <summary>
/// Abstract record, which should be self-referencing to be able to Create resource entries of the correct record type.
/// Override the ToFhirJson and ToResource methods to convert the resource to and from Fhir Json representation.
/// </summary>
public abstract record ResourceContent<TResourceContent> : IResourceContent
    where TResourceContent : ResourceContent<TResourceContent>, new()
{
    /// <summary>
    /// Create an empty JsonNode representing an empty resource.
    /// </summary>
    protected static JsonNode EmptyNode => new JsonObject();

    private IResource? resource;

    /// <summary>
    /// Creates a new ResourceContent of the correct type with the given resource interface.
    /// </summary>
    public static TResourceContent Create(IResource resource)
    {
        return new TResourceContent()
        {
            Value = resource
        };
    }

    /// <summary>
    /// Gets or sets the resource contained in this ResourceContent as the not-nullable type.
    /// </summary>
    public IResource Value { get => resource ?? throw new InvalidOperationException("Resource not set on ResourceContent"); set => resource = value; }


    /// <summary>
    /// Override to convert a JsonElement to the resource type contained in this ResourceContent.
    /// </summary>
    public abstract List<OutcomeIssue> ToResource(JsonElement jsonElement, string elementName, FhirVersion fhirVersion);
    /// <summary>
    /// Override to convert the resource contained in this ResourceContent to a JsonNode.
    /// </summary>
    public abstract (JsonNode, List<OutcomeIssue>) ToFhirJson(FhirVersion fhirVersion);

    /// <summary>
    /// Gets the resourceType property from a JsonElement if it exists, otherwise returns null.
    /// </summary>
    protected static string? GetResourceType(JsonElement jsonElement)
    {
        if (jsonElement.TryGetProperty("resourceType", out JsonElement result))
            return result.GetString();
        return null;
    }

    /// <summary>
    /// Helper to create an OutcomeIssue list indicating that the resourceType property is missing.
    /// </summary>
    protected static List<OutcomeIssue> ResourceTypeMissing(string elementName)
    {
        return [new OutcomeIssue(
            new OutcomeCode("invalid"),
            new OutcomeDetails(new OutcomeText($"Resource type is missing in {elementName}.")))];
    }
    /// <summary>
    /// Helper to create an OutcomeIssue list indicating that the resourceType is not supported by this ResourceContent type.
    /// </summary>
    protected (IResource?, List<OutcomeIssue>) ResourceTypeNotSupported(string elementName, string resourceType)
    {
        return (null, [new OutcomeIssue(
                new OutcomeCode("not-supported"),
                new OutcomeDetails(
                    new OutcomeText($"Resource type '{resourceType}' is not supported by {GetType().FullName} for {elementName}")))]);
    }
    /// <summary>
    /// Helper to create an OutcomeIssue list indicating that the resourceType is not supported by this ResourceContent type.
    /// </summary>
    protected (JsonNode, List<OutcomeIssue>) ResourceTypeNotSupported(IResource resource)
    {
        return (EmptyNode, [new OutcomeIssue(
                new OutcomeCode("not-supported"),
                new OutcomeDetails(
                    new OutcomeText($"Resource type '{resource.GetType().Name}' is not supported by {GetType().FullName}.")))]);
    }
}
