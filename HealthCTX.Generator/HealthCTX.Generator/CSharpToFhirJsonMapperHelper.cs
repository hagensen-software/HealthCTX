using System.Text;

namespace HealthCTX.Generator;

internal class CSharpToFhirJsonMapperHelper
{
    internal static void AddToFhirJsonMapper(RecordModel recordModel, StringBuilder sb)
    {
        StartToFhirJsonMethod(recordModel, sb);
        if (recordModel.FhirType == FhirType.Primitive)
            AddPropertiesForPrimitive(recordModel, sb);
        else
        {
            if (recordModel.FhirType == FhirType.Resource)
                AddNewJsonObjectForResource(recordModel, sb);
            else
                AddNewJsonObjectForElement(recordModel, sb);

            AddPropertiesForElement(recordModel, sb);
            if (recordModel.FhirType != FhirType.Resource)
                AddJsonObjectToParentObject(recordModel, sb);
        }
        if (recordModel.FhirType == FhirType.Resource)
            ReturnJsonObjectForElement(recordModel, sb);

        EndMethod(sb);
    }

    private static void StartToFhirJsonMethod(RecordModel recordModel, StringBuilder sb)
    {
        if (recordModel.FhirType == FhirType.Resource)
        {
            sb.AppendLine(
$$"""
    public static string ToFhirJson({{recordModel.RecordName}} {{recordModel.RecordInstanceName}}, HealthCTX.Domain.Attributes.FhirVersion fhirVersion = HealthCTX.Domain.Attributes.FhirVersion.R4)
    {
""");
        }
        else
        {
            sb.AppendLine(
$$"""
    public static JsonNode ToFhirJson({{recordModel.RecordName}} {{recordModel.RecordInstanceName}}, HealthCTX.Domain.Attributes.FhirVersion fhirVersion)
    {
""");
        }
    }

    private static void AddPropertiesForPrimitive(RecordModel recordModel, StringBuilder sb)
    {
        foreach (var propertyModel in recordModel.Properties) // Doesn't look right ;-)
        {
            sb.AppendLine(
$$"""
        return {{recordModel.RecordInstanceName}}.{{propertyModel.Name}}{{propertyModel.GetGetter()}};
""");
        }
    }

    private static void AddNewJsonObjectForResource(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""
        var {{recordModel.RecordInstanceName}}Object = new JsonObject
        {
            { "resourceType", "{{recordModel.ResourceName}}" }
        };

""");
    }

    private static void AddNewJsonObjectForElement(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""
        var {{recordModel.RecordInstanceName}}Object = new JsonObject();

""");
    }

    private static void AddJsonObjectToParentObject(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""

        return {{recordModel.RecordInstanceName}}Object;
""");
    }

    private static void AddPropertiesForElement(RecordModel recordModel, StringBuilder sb)
    {
        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonDeclaration(sb, propertyModel);
        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonBody(sb, recordModel.RecordInstanceName, propertyModel);
        foreach (var propertyModel in recordModel.Properties)
            AppendAddToJsonAssignmentOfDeclared(sb, recordModel.RecordInstanceName, propertyModel);
    }

    private static void AppendAddToJsonDeclaration(StringBuilder sb, PropertyModel propertyModel)
    {
        if (propertyModel.ElementName == string.Empty)
            return;

        if (propertyModel.FhirArray)
        {
            sb.AppendLine(
$$"""
        var {{propertyModel.ElementName}}Array = new JsonArray();
""");
        }
    }

    private static void AppendAddToJsonBody(StringBuilder sb, string recordInstanceName, PropertyModel propertyModel)
    {
        if (propertyModel.ElementName == string.Empty)
            return;

        if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
        {
            sb.Append(
$$"""
        if (fhirVersion is >= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} and <= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
     
""");
        }

        if (propertyModel.Enumerable)
        {
            sb.AppendLine(
$$"""
        {{recordInstanceName}}.{{propertyModel.Name}}.ForEach(p => {{propertyModel.ElementName}}Array.Add({{propertyModel.Type}}FhirJsonMapper.ToFhirJson(p, fhirVersion)));
""");
        }
        else
        {
            if (propertyModel.FhirArray)
            {
                sb.AppendLine(
$$"""
        {{propertyModel.ElementName}}Array.Add({{propertyModel.Type}}FhirJsonMapper.ToFhirJson({{recordInstanceName}}.{{propertyModel.Name}}, fhirVersion));

""");
            }
            else
            {
                if (!propertyModel.Required)
                {
                    sb.Append(
$$"""
        if ({{recordInstanceName}}.{{propertyModel.Name}} is not null)
    
""");
                }
                sb.AppendLine(
$$"""
        {{recordInstanceName}}Object.Add("{{propertyModel.ElementName}}", {{propertyModel.Type}}FhirJsonMapper.ToFhirJson({{recordInstanceName}}.{{propertyModel.Name}}, fhirVersion));
""");
            }
        }
    }

    public static void AppendAddToJsonAssignmentOfDeclared(StringBuilder sb, string recordInstanceName, PropertyModel propertyModel)
    {
        if (propertyModel.FhirArray)
        {
            if (propertyModel.FromVersion > FhirVersion.R4 || propertyModel.ToVersion < FhirVersion.R5)
            {
                sb.Append(
    $$"""
        if (fhirVersion is >= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.FromVersion}} and <= HealthCTX.Domain.Attributes.FhirVersion.{{propertyModel.ToVersion}})
     
""");
            }

            sb.AppendLine(
$$"""
        {{recordInstanceName}}Object.Add("{{propertyModel.ElementName}}", {{propertyModel.ElementName}}Array);
""");
        }
    }

    private static void ReturnJsonObjectForElement(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""

        return {{recordModel.RecordInstanceName}}Object.ToJsonString();
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
