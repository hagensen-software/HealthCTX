using System.Text;

namespace HealthCTX.Generator;

internal class CSharpFhirJsonMapperHelper
{
    internal static void StartMapperClass(RecordModel recordModel, StringBuilder sb)
    {
        sb.AppendLine(
$$"""
using HealthCTX.Domain.OperationOutcomes;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
""");
        foreach (var ns in recordModel.PropertyNamespaces)
        {
            sb.AppendLine($"using {ns};");
        }

        sb.AppendLine(
$$"""

namespace {{recordModel.RecordNamespace}};

/// <summary>
/// Mapper class to convert {{recordModel.RecordName}} to and from FHIR JSON representation.
/// </summary>
public static class {{recordModel.RecordName}}FhirJsonMapper
{
""");
    }

    internal static void EndClass(StringBuilder sb)
    {
        sb.Append(
$$"""
}
""");
    }
}
