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
            AddOperationalOutcomeDeclaration(sb);
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
    /// <summary>
    /// Converts a JSON string to a {{recordModel.RecordName}} resource.
    /// </summary>
    public static ({{recordModel.RecordName}}?, OperationOutcome) To{{recordModel.RecordName}}(string jsonString, HealthCTX.Domain.Attributes.FhirVersion fhirVersion = HealthCTX.Domain.Attributes.FhirVersion.R4)
    {
        try
        {
            var document = JsonDocument.Parse(jsonString);

            (var resource, var issues) = To{{recordModel.RecordName}}(document.RootElement, "{{recordModel.ResourceName}}", fhirVersion);
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
    /// <summary>
    /// Converts a JsonElement to a {{recordModel.RecordName}} element.
    /// </summary>
    public static ({{recordModel.RecordName}}?, List<OutcomeIssue>) To{{recordModel.RecordName}}(JsonElement jsonElement, string elementName, HealthCTX.Domain.Attributes.FhirVersion fhirVersion)
    {
""");
    }

    private static void AddPropertiesForPrimitive(RecordModel recordModel, StringBuilder sb)
    {
        foreach (var propertyModel in recordModel.Properties) // Doesn't look right ;-)
        {
            if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
            {
                sb.Append(
$$"""
        if (fhirVersion is < HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} or > HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
            return (null, [OutcomeIssue.CreateStructureError($"Error parsing {elementName}. Property '{{propertyModel.ElementName}}' not supported in FHIR version {fhirVersion}.")]);

""");
            }

            switch (propertyModel.Type)
            {
                case "System.Boolean":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.True and not JsonValueKind.False)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected boolean value.")]);

        var value = jsonElement.GetBoolean();

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;

                case "System.String":
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
                case "System.DateTimeOffset":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.String)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected string value.")]);

        var value = jsonElement.GetString();
        if (!System.DateTimeOffset.TryParse(value, out var dateTimeValue))
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected valid datetime value.")]);

        return (new {{recordModel.RecordName}}(dateTimeValue), []);
""");
                    break;
                case "System.DateOnly":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.String)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected string value.")]);

        var value = jsonElement.GetString();
        if (!System.DateOnly.TryParse(value, out var dateValue))
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected valid date value.")]);

        return (new {{recordModel.RecordName}}(dateValue), []);
""");
                    break;
                case "System.TimeOnly":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.String)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected string value.")]);

        var value = jsonElement.GetString();
        if (!System.TimeOnly.TryParse(value, out var timeValue))
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected valid time value.")]);

        return (new {{recordModel.RecordName}}(timeValue), []);
""");
                    break;
                case "System.Int32":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.Number)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected integer value.")]);

        var value = jsonElement.GetInt32();

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;
                case "System.UInt32":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.Number)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected unsigned integer value.")]);

        var value = jsonElement.GetUInt32();

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;
                case "System.Int64":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.Number)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected integer value.")]);

        var value = jsonElement.GetInt64();

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;
                case "System.Double":
                    sb.AppendLine(
$$"""
        if (jsonElement.ValueKind is not JsonValueKind.Number)
            return (null, [OutcomeIssue.CreateValueError($"Error parsing {elementName}. Expected double value.")]);

        var value = jsonElement.GetDouble();

        return (new {{recordModel.RecordName}}(value), []);
""");
                    break;

            }
            ;
        }
    }

    private static void AddOperationalOutcomeDeclaration(StringBuilder sb)
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
            sb.AppendLine(
$$"""

        {{propertyModel.Type}}{{propertyModel.TypeArguments}}? {{GetInstanceName(propertyModel)}} = null;
""");
            if (propertyModel.FhirArray)
            {
                sb.AppendLine(
$$"""
        List<{{propertyModel.Type}}>? {{propertyModel.Type.Split('.').Last().ToLower()}}List = [];
        if (jsonElement.TryGetProperty("{{propertyModel.ElementName}}", out JsonElement {{propertyModel.ElementName}}Array))
        {
""");
                if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
                {
                    sb.AppendLine(
$$"""
            if (fhirVersion is < HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} or > HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
            {
                outcomes.Add(OutcomeIssue.CreateStructureError($"Error parsing {elementName}. Property '{{propertyModel.ElementName}}' not supported in FHIR version {fhirVersion}."));
            }
            else
            {
""");
                }

                    sb.AppendLine(
$$"""
            if ({{propertyModel.ElementName}}Array.ValueKind == JsonValueKind.Array)
            {
                foreach (var arrayElement in {{propertyModel.ElementName}}Array.EnumerateArray())
                {
""");
                if (!propertyModel.Enumerable)
                {
                    sb.AppendLine(
$$"""
                    if ({{GetInstanceName(propertyModel)}} is not null)
                        outcomes.Add(OutcomeIssue.CreateStructureError($"Error parsing {elementName}. Too many elements in array."));

""");
                }
                sb.AppendLine(
$$"""
                    ({{GetInstanceName(propertyModel)}}, var {{propertyModel.Name.ToLower()}}Outcomes) = {{propertyModel.Type}}FhirJsonMapper{{propertyModel.TypeArguments}}.To{{propertyModel.Type.Split('.').Last()}}(arrayElement, "{{propertyModel.ElementName}}", fhirVersion);
                    outcomes.AddRange({{propertyModel.Name.ToLower()}}Outcomes);
""");
                if (propertyModel.Enumerable)
                {
                    sb.AppendLine(
$$"""

                    if ({{GetInstanceName(propertyModel)}} is null)
                        continue;

                    {{propertyModel.Type.Split('.').Last().ToLower()}}List.Add({{GetInstanceName(propertyModel)}});
""");
                }
                sb.AppendLine(
$$"""
                }
            }
            else
                outcomes.Add(OutcomeIssue.CreateStructureError($"Error parsing {elementName}. Expected an array."));
""");
                    sb.AppendLine(
"""
            }
""");

                if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
                {
                    sb.AppendLine(
$$"""
        }
""");
                }

            }
            else
            {
                sb.AppendLine(
$$"""
        if (jsonElement.TryGetProperty("{{propertyModel.ElementName}}", out JsonElement {{propertyModel.Name.ToLower()}}Object))
        {
""");
                if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
                {
                    sb.AppendLine(
$$"""
            if (fhirVersion is < HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} or > HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
            {
                outcomes.Add(OutcomeIssue.CreateStructureError($"Error parsing {elementName}. Property '{{propertyModel.ElementName}}' not supported in FHIR version {fhirVersion}."));
            }
            else
            {
""");
                }
                if (propertyModel.HasDefaultConstructor)
                {
                    sb.AppendLine(
    $$"""
            {{GetInstanceName(propertyModel)}} = new();
            var {{propertyModel.Name.ToLower()}}Outcomes = {{GetInstanceName(propertyModel)}}.ToResource({{propertyModel.Name.ToLower()}}Object, "{{propertyModel.ElementName}}", fhirVersion);
            outcomes.AddRange({{propertyModel.Name.ToLower()}}Outcomes);
""");
                }
                else
                {
                    sb.AppendLine(
    $$"""
            ({{GetInstanceName(propertyModel)}}, var {{propertyModel.Name.ToLower()}}Outcomes) = {{propertyModel.Type}}FhirJsonMapper{{propertyModel.TypeArguments}}.To{{propertyModel.Type.Split('.').Last()}}({{propertyModel.Name.ToLower()}}Object, "{{propertyModel.ElementName}}", fhirVersion);
            outcomes.AddRange({{propertyModel.Name.ToLower()}}Outcomes);
""");
                }
                if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
                {
                    sb.AppendLine(
$$"""
            }
""");
                }
                sb.AppendLine(
$$"""
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
                paramNames.Add(GetInstanceName(propertyModel));
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
        if ({{GetInstanceName(property)}} is null)
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

    private static string GetInstanceName(PropertyModel model)
    {
        return model.Name.ToLower() + "Instance";
    }
}
