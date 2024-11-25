using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.FhirSupportGenerator;

public struct RecordModel(string recordName, string recordNamespace, string recordInstanceName, FhirType fhirType, PropertyModel[] properties, string? resourceName, IEnumerable<FhirGeneratorDiagnostic> diagnostics)
{
    private const string iElementInterface = "HealthCTX.Domain.Framework.Interfaces.IElement";

    public string RecordName { get; } = recordName;
    public string RecordNamespace { get; } = recordNamespace;
    public string RecordInstanceName { get; } = recordInstanceName;
    public FhirType FhirType { get; } = fhirType;
    public PropertyModel[] Properties { get; } = properties;
    public string? ResourceName { get; } = resourceName;
    public IEnumerable<FhirGeneratorDiagnostic> Diagnostics { get; } = diagnostics;

    public static RecordModel? Create(INamedTypeSymbol? recordSymbol)
    {
        var diagnostics = new List<FhirGeneratorDiagnostic>();

        if (recordSymbol is null || !ImplementsIElementInterface(recordSymbol))
            return null; // TODO: Detect if IElement was just forgotten

        var elementNamesByInterface = FhirAttributeHelper.GetApplicableProperties(recordSymbol.AllInterfaces);

        var fhirType = FhirAttributeHelper.GetFhirType(recordSymbol, out var resourceName);
        if (!fhirType.HasValue)
        {
            diagnostics.Add(new FhirGeneratorDiagnostic(
                "HCTX002",
                "Fhir Base Type not found",
                $"No interface for record {recordSymbol.ToDisplayString()} has a FhirResource, FhirElement, or FhirPrimitive attribute",
                "FhirGenerator",
                DiagnosticSeverity.Warning,
                recordSymbol.Locations.FirstOrDefault() ?? Location.None
                ));
            return null;
        }

        var members = recordSymbol.GetMembers()
            .Where(m => m.Kind == SymbolKind.Property)
            .Where(m => ((IPropertySymbol)m).Name != "EqualityContract")
            .Select(m => (IPropertySymbol)m);
        var props = members.Select(m => PropertyModel.Create(m, elementNamesByInterface));

        foreach (var prop in props)
            diagnostics.AddRange(prop.Item2);

        var properties = props.Select(p => p.Item1).OfType<PropertyModel>();

        return new RecordModel(recordSymbol.Name, recordSymbol.ContainingNamespace.ToDisplayString(), recordSymbol.Name.ToLower(), fhirType.Value, properties.ToArray(), resourceName, diagnostics);
    }

    private static bool ImplementsIElementInterface(INamedTypeSymbol classSymbol)
    {
        return classSymbol?.AllInterfaces.Any(i => i.ToDisplayString() == iElementInterface) ?? false;
    }
}

