using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.Generator;

public struct RecordModel(string recordName, string recordNamespace, string recordInstanceName, FhirType fhirType, PropertyModel[] properties, string? resourceName)
{
    private const string iElementInterface = "HealthCTX.Domain.Framework.Interfaces.IElement";

    public string RecordName { get; } = recordName;
    public string RecordNamespace { get; } = recordNamespace;
    public string RecordInstanceName { get; } = recordInstanceName;
    public FhirType FhirType { get; } = fhirType;
    public PropertyModel[] Properties { get; } = properties;
    public string? ResourceName { get; } = resourceName;

    public static (RecordModel?, IEnumerable<FhirGeneratorDiagnostic>) Create(INamedTypeSymbol? recordSymbol)
    {
        var diagnostics = new List<FhirGeneratorDiagnostic>();

        if (recordSymbol is null)
            return (null, []);

        var implementsIElement = ImplementsIElementInterface(recordSymbol);
        var fhirType = FhirAttributeHelper.GetFhirType(recordSymbol, out var resourceName);

        if (!fhirType.HasValue)
        {
            if (!implementsIElement)
                return (null, []);
            else
            {
                diagnostics.Add(new FhirGeneratorDiagnostic(
                    "HCTX002",
                    "Fhir Base Type not found",
                    $"No interface for record {recordSymbol.ToDisplayString()} has a FhirResource, FhirElement, or FhirPrimitive attribute",
                    "FhirGenerator",
                    DiagnosticSeverity.Error,
                    recordSymbol.Locations.FirstOrDefault() ?? Location.None
                    ));
                return (null, diagnostics);
            }
        }
        else if (!implementsIElement)
        {
            diagnostics.Add(new FhirGeneratorDiagnostic(
                "HCTX001",
                "Does not implement IElement",
                $"{recordSymbol.ToDisplayString()} has a FhirResource, FhirElement, or FhirPrimitive attribute, but does not implement the IElement interface",
                "FhirGenerator",
                DiagnosticSeverity.Error,
                recordSymbol.Locations.FirstOrDefault() ?? Location.None
                ));
            return (null, diagnostics);
        }

        var elementNamesByInterface = FhirAttributeHelper.GetApplicableProperties(recordSymbol.AllInterfaces);


        var members = recordSymbol.GetMembers()
            .Where(m => m.Kind == SymbolKind.Property)
            .Where(m => ((IPropertySymbol)m).Name != "EqualityContract")
            .Select(m => (IPropertySymbol)m);
        var props = members.Select(m => PropertyModel.Create(m, elementNamesByInterface));

        foreach (var prop in props)
            diagnostics.AddRange(prop.Item2);

        var properties = props.Select(p => p.Item1).OfType<PropertyModel>();

        return (new RecordModel(recordSymbol.Name, recordSymbol.ContainingNamespace.ToDisplayString(), recordSymbol.Name.ToLower(), fhirType.Value, properties.ToArray(), resourceName), diagnostics);
    }

    private static bool ImplementsIElementInterface(INamedTypeSymbol classSymbol)
    {
        return classSymbol?.AllInterfaces.Any(i => i.ToDisplayString() == iElementInterface) ?? false;
    }
}

