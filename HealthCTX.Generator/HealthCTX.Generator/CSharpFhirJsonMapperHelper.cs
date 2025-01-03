﻿using System.Text;

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

namespace {{recordModel.RecordNamespace}};

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
