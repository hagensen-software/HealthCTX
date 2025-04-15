using System.Text;

namespace HealthCTX.Generator;

internal class CSharpToFhirJsonMapperHelper
{
    internal static void AddToFhirJsonMapper(RecordModel recordModel, StringBuilder sb)
    {
        var spaces = StartToFhirJsonMethod(recordModel, sb);
        if (recordModel.FhirType == FhirType.Primitive)
            spaces = AddPropertiesForPrimitive(recordModel, sb, spaces);
        else
        {
            if (recordModel.FhirType == FhirType.Resource)
                spaces = AddNewJsonObjectForResource(recordModel, sb, spaces);
            else
                spaces = AddNewJsonObjectForElement(recordModel, sb, spaces);

            spaces = AddPropertiesForElement(recordModel, sb, spaces);
            if (recordModel.FhirType != FhirType.Resource)
                AddJsonObjectToParentObject(recordModel, sb);

            if (recordModel.FhirType == FhirType.Resource)
                ReturnJsonObjectForElement(recordModel, sb, spaces);
        }

        EndMethod(sb);
    }

    private static int StartToFhirJsonMethod(RecordModel recordModel, StringBuilder sb)
    {
        if (recordModel.FhirType == FhirType.Resource)
        {
            sb.AppendLine(
$$"""
    public static (string?, OperationOutcome) ToFhirJson({{recordModel.RecordName}} {{recordModel.RecordInstanceName}}, HealthCTX.Domain.Attributes.FhirVersion fhirVersion = HealthCTX.Domain.Attributes.FhirVersion.R4)
    {
        try
        {
""");
            return 12;
        }
        else
        {
            sb.AppendLine(
$$"""
    public static (JsonNode, List<OutcomeIssue>) ToFhirJson({{recordModel.RecordName}} {{recordModel.RecordInstanceName}}, HealthCTX.Domain.Attributes.FhirVersion fhirVersion)
    {
        List<OutcomeIssue> outcomes = [];

""");
            return 8;
        }
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

{{Indent(spaces)}}List<OutcomeIssue> outcomes = [];

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
        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonDeclaration(sb, propertyModel, spaces);
        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonBody(sb, recordModel.RecordInstanceName, propertyModel, spaces);
        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonAssignmentOfDeclared(sb, recordModel.RecordInstanceName, propertyModel, spaces);

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
{{Indent(spaces)}}    (var elementNode, var elementOutcomes) = {{propertyModel.Type}}FhirJsonMapper.ToFhirJson(p, fhirVersion);
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
{{Indent(spaces)}}    (var {{propertyModel.ElementName}}Node, var {{propertyModel.ElementName}}Outcomes) = {{propertyModel.Type}}FhirJsonMapper.ToFhirJson({{recordInstanceName}}.{{propertyModel.Name}}, fhirVersion);
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
                var instanceName = propertyModel.Name.ToLower();
                sb.AppendLine(
$$"""
{{Indent(spaces)}}(var {{instanceName}}Node, var {{instanceName}}Outcomes) = {{propertyModel.Type}}FhirJsonMapper.ToFhirJson({{recordInstanceName}}.{{propertyModel.Name}}, fhirVersion);
{{Indent(spaces)}}outcomes.AddRange({{instanceName}}Outcomes);
{{Indent(spaces)}}{{recordInstanceName}}Object.Add("{{propertyModel.ElementName}}", {{instanceName}}Node);
""");

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

    private static void ReturnJsonObjectForElement(RecordModel recordModel, StringBuilder sb, int spaces)
    {
        spaces = spaces - 4;
        sb.AppendLine(
$$"""

{{Indent(spaces)}}    return ({{recordModel.RecordInstanceName}}Object.ToJsonString(), new OperationOutcome([..outcomes]));
{{Indent(spaces)}}}
{{Indent(spaces)}}catch (Exception ex)
{{Indent(spaces)}}{
{{Indent(spaces)}}    return (null, new OperationOutcome([OutcomeIssue.CreateStructureError(ex.Message)]));
{{Indent(spaces)}}}
""");
    }

    private static void EndMethod(StringBuilder sb)
    {
        sb.AppendLine(
$$"""
    }

""");
    }

    private static string Indent(int spaces) => new string(' ', spaces);
}
