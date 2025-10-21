using System.Collections.Generic;
using System.Text;

namespace HealthCTX.Generator;

internal class CSharpToFhirJsonMapperHelper
{
    internal static void AddToFhirJsonMapper(RecordModel recordModel, StringBuilder sb)
    {
        var spaces = StartToFhirJsonMethod(recordModel, sb);
        if (recordModel.FhirType == FhirType.Primitive)
            _ = AddPropertiesForPrimitive(recordModel, sb, spaces);
        else
        {
            if (recordModel.FhirType == FhirType.Resource)
                spaces = AddNewJsonObjectForResource(recordModel, sb, spaces);
            else
                spaces = AddNewJsonObjectForElement(recordModel, sb, spaces);

            _ = AddPropertiesForElement(recordModel, sb, spaces);
            AddJsonObjectToParentObject(recordModel, sb);
        }

        EndMethod(sb);
    }

    private static int StartToFhirJsonMethod(RecordModel recordModel, StringBuilder sb)
    {
        if (recordModel.FhirType == FhirType.Resource)
        {
            sb.AppendLine(
$$"""
    /// <summary>
    /// Converts the {{recordModel.RecordName}} to a FHIR JSON string representation.
    /// </summary>
    public static (string?, OperationOutcome) ToFhirJsonString(this {{recordModel.RecordTypeName}} {{recordModel.RecordInstanceName}}, HealthCTX.Domain.Attributes.FhirVersion fhirVersion = HealthCTX.Domain.Attributes.FhirVersion.R4)
    {
        try
        {
            (var jsonNode, var outcomes) = ToFhirJson({{recordModel.RecordInstanceName}}, fhirVersion);

            return (jsonNode.ToJsonString(), new OperationOutcome([..outcomes]));
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
    /// Converts the {{recordModel.RecordName}} to a FHIR JSON representation.
    /// </summary>
    public static (JsonNode, List<OutcomeIssue>) ToFhirJson(this {{recordModel.RecordTypeName}} {{recordModel.RecordInstanceName}}, HealthCTX.Domain.Attributes.FhirVersion fhirVersion)
    {
        List<OutcomeIssue> outcomes = [];

""");
        return 8;
    }

    private static int AddPropertiesForPrimitive(RecordModel recordModel, StringBuilder sb, int spaces)
    {
        foreach (var propertyModel in recordModel.Properties) // Doesn't look right ;-)
        {
            sb.AppendLine(
$$"""
{{Indent(spaces)}}return ({{recordModel.RecordInstanceName}}.{{propertyModel.Name}}{{propertyModel.GetGetter()}}, outcomes);
""");
        }
        return spaces - 4;
    }

    private static int AddNewJsonObjectForResource(RecordModel recordModel, StringBuilder sb, int spaces)
    {
        sb.AppendLine(
$$"""
{{Indent(spaces)}}var {{recordModel.RecordInstanceName}}Object = new JsonObject
{{Indent(spaces)}}{
{{Indent(spaces)}}    { "resourceType", "{{recordModel.ResourceName}}" }
{{Indent(spaces)}}};

""");
        return spaces;
    }

    private static int AddNewJsonObjectForElement(RecordModel recordModel, StringBuilder sb, int spaces)
    {
        sb.AppendLine(
$$"""
{{Indent(spaces)}}var {{recordModel.RecordInstanceName}}Object = new JsonObject();

""");
        return spaces;
    }

    private static void AddJsonObjectToParentObject(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""

        return ({{recordModel.RecordInstanceName}}Object, outcomes);
""");
    }

    private static int AddPropertiesForElement(RecordModel recordModel, StringBuilder sb, int spaces)
    {
        var alreadyDeclared = new HashSet<string>();
        foreach (var propertyModel in recordModel.Properties)
        {
            if (alreadyDeclared.Contains(propertyModel.ElementName))
                continue;

            AppendAddToJsonDeclaration(sb, propertyModel, spaces);

            alreadyDeclared.Add(propertyModel.ElementName);
        }

        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonBody(sb, recordModel.RecordInstanceName, propertyModel, spaces);

        alreadyDeclared.Clear();
        foreach (var propertyModel in recordModel.Properties)
        {
            if (alreadyDeclared.Contains(propertyModel.ElementName))
                continue;

            AppendAddToJsonAssignmentOfDeclared(sb, recordModel.RecordInstanceName, propertyModel, spaces);

            alreadyDeclared.Add(propertyModel.ElementName);
        }


        return spaces;
    }

    private static int AppendAddToJsonDeclaration(StringBuilder sb, PropertyModel propertyModel, int spaces)
    {
        if (propertyModel.ElementName == string.Empty)
            return spaces;

        if (propertyModel.FhirArray)
        {
            sb.AppendLine(
$$"""
{{Indent(spaces)}}var {{propertyModel.ElementName}}Array = new JsonArray();
""");
        }

        return spaces;
    }

    private static void AppendAddToJsonBody(StringBuilder sb, string recordInstanceName, PropertyModel propertyModel, int spaces)
    {
        if (propertyModel.ElementName == string.Empty)
            return;

        if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
        {
            sb.AppendLine(
$$"""
{{Indent(spaces)}}if (fhirVersion is >= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} and <= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
{{Indent(spaces)}}{
""");
            spaces += 4;
        }

        if (propertyModel.Enumerable)
        {
            sb.AppendLine(
$$"""
{{Indent(spaces)}}{{recordInstanceName}}.{{propertyModel.Name}}.ForEach(p =>
{{Indent(spaces)}}{
{{Indent(spaces)}}    (var elementNode, var elementOutcomes) = {{propertyModel.Type}}FhirJsonMapper{{propertyModel.TypeArguments}}.ToFhirJson(p, fhirVersion);
{{Indent(spaces)}}    outcomes.AddRange(elementOutcomes);
{{Indent(spaces)}}    {{propertyModel.ElementName}}Array.Add(elementNode);
{{Indent(spaces)}}});
""");
        }
        else
        {
            if (propertyModel.FhirArray)
            {
                sb.AppendLine(
$$"""
{{Indent(spaces)}}if ({{recordInstanceName}}.{{propertyModel.Name}} is not null)
{{Indent(spaces)}}{
{{Indent(spaces)}}    (var {{propertyModel.ElementName}}Node, var {{propertyModel.ElementName}}Outcomes) = {{recordInstanceName}}.{{propertyModel.Name}}.ToFhirJson(fhirVersion);
{{Indent(spaces)}}    outcomes.AddRange({{propertyModel.ElementName}}Outcomes);
{{Indent(spaces)}}    {{propertyModel.ElementName}}Array.Add({{propertyModel.ElementName}}Node);
{{Indent(spaces)}}}

""");
            }
            else
            {
                if (!propertyModel.Required)
                {
                    sb.AppendLine(
$$"""
{{Indent(spaces)}}if ({{recordInstanceName}}.{{propertyModel.Name}} is not null)
{{Indent(spaces)}}{
""");
                    spaces += 4;
                }
                if (propertyModel.FixedValue is null)
                {
                    var instanceName = propertyModel.Name.ToLower();
                    sb.AppendLine(
$$"""
{{Indent(spaces)}}(var {{instanceName}}Node, var {{instanceName}}Outcomes) = {{recordInstanceName}}.{{propertyModel.Name}}.ToFhirJson(fhirVersion);
{{Indent(spaces)}}outcomes.AddRange({{instanceName}}Outcomes);
{{Indent(spaces)}}{{recordInstanceName}}Object.Add("{{propertyModel.ElementName}}", {{instanceName}}Node);
""");
                }
                else
                {
                    sb.AppendLine(
                    $$"""
{{Indent(spaces)}}{{recordInstanceName}}Object.Add("{{propertyModel.ElementName}}", "{{propertyModel.FixedValue}}");
""");
                }

                if (!propertyModel.Required)
                {
                    spaces -= 4;
                    sb.AppendLine(
$$"""
{{Indent(spaces)}}}
""");
                }
            }
        }

        if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
        {
            spaces -= 4;
            sb.AppendLine(
$$"""
{{Indent(spaces)}}}
""");
        }
    }

    public static void AppendAddToJsonAssignmentOfDeclared(StringBuilder sb, string recordInstanceName, PropertyModel propertyModel, int spaces)
    {
        if (propertyModel.FhirArray)
        {
            if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
            {
                sb.AppendLine(
$$"""
{{Indent(spaces)}}if (fhirVersion is >= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} and <= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
{{Indent(spaces)}}{
""");
                spaces += 4;
            }

            sb.AppendLine(
$$"""
{{Indent(spaces)}}{{recordInstanceName}}Object.Add("{{propertyModel.ElementName}}", {{propertyModel.ElementName}}Array);
""");

            if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
            {
                spaces -= 4;
                sb.AppendLine(
$$"""
{{Indent(spaces)}}}
""");
            }
        }
    }

    private static void EndMethod(StringBuilder sb)
    {
        sb.AppendLine(
$$"""
    }

""");
    }

    private static string Indent(int spaces) => new(' ', spaces);
}
