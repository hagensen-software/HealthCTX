using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthCTX.Generator;

internal class CSharpFromFhirJsonMapperHelper
{
    internal static void AddFromFhirJsonMapper(RecordModel recordModel, StringBuilder sb)
    {
        StartFromJsonMethod(recordModel, sb);

        if (recordModel.FhirType == FhirType.Primitive)
            AddPropertiesForPrimitive(recordModel, sb);
        else
        {
            AddOperationalOutcomeDeclaration(recordModel, sb);
            AddPropertiesForElement(recordModel, sb);
            AddReturnResult(recordModel, sb);
        }

        EndMethod(sb);
    }

    private static void StartFromJsonMethod(RecordModel recordModel, StringBuilder sb)
    {
        if (recordModel.FhirType == FhirType.Resource)
        {
            sb.AppendLine(
$$"""
    public static ({{recordModel.RecordName}}?, OperationOutcome) To{{recordModel.RecordName}}(string jsonString)
    {
        try
        {
            var document = JsonDocument.Parse(jsonString);

            (var resource, var issues) = To{{recordModel.RecordName}}(document.RootElement, "{{recordModel.ResourceName}}");
            return (resource, new OperationOutcome([..issues]));
        }
        catch (Exception ex)
        {
            return (null, new OperationOutcome([OutcomeIssue.CreateStructureError(ex.Message)]));
        }
    }

""");
        }
        sb.AppendLine(
$$"""
    public static ({{recordModel.RecordName}}?, List<OutcomeIssue>) To{{recordModel.RecordName}}(JsonElement jsonElement, string elementName)
    {
""");
    }

    private static void AddPropertiesForPrimitive(RecordModel recordModel, StringBuilder sb)
    {
        foreach (var propertyModel in recordModel.Properties) // Doesn't look right ;-)
        {
            switch (propertyModel.Type)
            {
                case "bool":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.True and not JsonValueKind.False)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected boolean value.")]);

        var value = jsonElement.GetBoolean();

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;

                case "string":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.String)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected string value.")]);

        var value = jsonElement.GetString()!;

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;
                case "System.Uri":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.String)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected string value.")]);

        var value = jsonElement.GetString();
        if (!System.Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var uriValue))
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected valid URI value.")]);

        return (new {{recordModel.RecordName}}(uriValue), []);
""");
                    break;

            };
        }
    }

    private static void AddOperationalOutcomeDeclaration(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""
        List<OutcomeIssue> outcomes = [];
""");
    }

    private static void AddPropertiesForElement(RecordModel recordModel, StringBuilder sb)
    {
        foreach (var propertyModel in recordModel.Properties)
        {
            if (propertyModel.Enumerable)
            {
                sb.AppendLine(
$$"""

        List<{{propertyModel.Type}}>? {{propertyModel.Type.Split('.').Last().ToLower()}}List = [];
        if (jsonElement.TryGetProperty("{{propertyModel.ElementName}}", out JsonElement {{propertyModel.ElementName}}Array))
        {
            if ({{propertyModel.ElementName}}Array.ValueKind == JsonValueKind.Array)
            {
                foreach (var arrayElement in {{propertyModel.ElementName}}Array.EnumerateArray())
                {
                    (var {{propertyModel.Name.ToLower()}}, var {{propertyModel.Name.ToLower()}}Outcomes) = {{propertyModel.Type}}FhirJsonMapper.To{{propertyModel.Type.Split('.').Last()}}(arrayElement, "{{propertyModel.ElementName}}");
                    outcomes.AddRange({{propertyModel.Name.ToLower()}}Outcomes);

                    if ({{propertyModel.Name.ToLower()}} is null)
                        continue;

                    {{propertyModel.Type.Split('.').Last().ToLower()}}List.Add({{propertyModel.Name.ToLower()}});
                }
            }
            else
                outcomes.Add(OutcomeIssue.CreateStructureError($"Error parsing {elementName}. Expected an array."));
        }
""");
            }
            else
            {
                sb.AppendLine(
$$"""

        {{propertyModel.Type}}? {{propertyModel.Name.ToLower()}} = null;
        if (jsonElement.TryGetProperty("{{propertyModel.ElementName}}", out JsonElement {{propertyModel.Name.ToLower()}}Object))
        {
            ({{propertyModel.Name.ToLower()}}, var {{propertyModel.Name.ToLower()}}Outcomes) = {{propertyModel.Type}}FhirJsonMapper.To{{propertyModel.Type.Split('.').Last()}}({{propertyModel.Name.ToLower()}}Object, "{{propertyModel.ElementName}}");
            outcomes.AddRange({{propertyModel.Name.ToLower()}}Outcomes);
        }
""");
            }
        }
    }

    private static void AddReturnResult(RecordModel recordModel, StringBuilder sb)
    {
        var paramNames = new List<string>();
        var nullCheckProperties = new List<PropertyModel>();
        foreach (var propertyModel in recordModel.Properties)
        {
            if (propertyModel.Enumerable)
                paramNames.Add($"[..{propertyModel.Type.Split('.').Last().ToLower()}List]");
            else
            {
                paramNames.Add(propertyModel.Name.ToLower());
                if (propertyModel.Required)
                    nullCheckProperties.Add(propertyModel);
            }
        }

        if (nullCheckProperties.Count > 0)
        {
            sb.AppendLine(
"""

        bool requiredOk = true;
""");
            foreach (var property in nullCheckProperties)
            {
                sb.AppendLine(
    $$"""
        if ({{property.Name.ToLower()}} is null)
        {
            outcomes.Add(OutcomeIssue.CreateRequiredError("Required element '{{property.ElementName}}' is missing"));
            requiredOk = false;
        }
""");
            }

            sb.AppendLine(
    """
        if (!requiredOk)
            return (null, outcomes);
""");
        }
        sb.AppendLine(
$$"""

#pragma warning disable CS8604
        return (new {{recordModel.RecordName}}({{string.Join(", ", paramNames)}}), outcomes);
#pragma warning restore CS8604
""");
    }

    private static void EndMethod(StringBuilder sb)
    {
        sb.AppendLine(
$$"""
    }
""");
    }
}
