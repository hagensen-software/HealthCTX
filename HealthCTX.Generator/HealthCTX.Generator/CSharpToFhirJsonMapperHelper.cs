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
    public static string ToFhirJson({{recordModel.RecordName}} {{recordModel.RecordInstanceName}})
    {
""");
        }
        else
        {
            sb.AppendLine(
$$"""
    public static JsonNode ToFhirJson({{recordModel.RecordName}} {{recordModel.RecordInstanceName}})
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
            AppendAddToJsonBody(sb, recordModel.RecordInstanceName, propertyModel.Name, propertyModel.ElementName, propertyModel.Type, propertyModel.Enumerable, propertyModel.FhirArray, propertyModel.Required);
    }

    private static void AppendAddToJsonBody(StringBuilder sb, string recordInstanceName, string propertyName, string elementName, string type, bool enumerable, bool fhirArray, bool required)
    {
        if (elementName == string.Empty)
            return;

        if (fhirArray)
        {
            sb.AppendLine(
$$"""
        var {{elementName}}Array = new JsonArray();
""");
        }

        if (enumerable)
        {
            sb.AppendLine(
$$"""
        {{recordInstanceName}}.{{propertyName}}.ForEach(p => {{elementName}}Array.Add({{type}}FhirJsonMapper.ToFhirJson(p)));
        {{recordInstanceName}}Object.Add("{{elementName}}", {{elementName}}Array);
""");
        }
        else
        {
            if (fhirArray)
            {
                sb.AppendLine(
$$"""
        {{elementName}}Array.Add({{type}}FhirJsonMapper.ToFhirJson({{recordInstanceName}}.{{propertyName}}));
        {{recordInstanceName}}Object.Add("{{elementName}}", {{elementName}}Array);

""");
            }
            else
            {
                if (!required)
                {
                    sb.Append(
$$"""
        if ({{recordInstanceName}}.{{propertyName}} is not null)
    
""");
                }
                sb.AppendLine(
$$"""
        {{recordInstanceName}}Object.Add("{{elementName}}", {{type}}FhirJsonMapper.ToFhirJson({{recordInstanceName}}.{{propertyName}}));
""");
            }
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
