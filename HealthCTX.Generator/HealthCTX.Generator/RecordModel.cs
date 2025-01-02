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

    public static (RecordModel?, IEnumerable<FhirGeneratorDiagnostic>) Create(INamedTypeSymbol recordSymbol)
    {
        var diagnostics = new List<FhirGeneratorDiagnostic>();

        if (recordSymbol is null)
            return (null, []);

        var implementsIElement = ImplementsIElementInterface(recordSymbol, diagnostics);
        var fhirType = FhirAttributeHelper.GetFhirType(recordSymbol, diagnostics, out var resourceName);

        if (!fhirType.HasValue || !implementsIElement)
            return (null, diagnostics);

        var elementNamesByInterface = FhirAttributeHelper.GetApplicableProperties(recordSymbol.AllInterfaces);

        var members = recordSymbol.GetMembers()
            .Where(m => m.Kind == SymbolKind.Property)
            .Where(m => ((IPropertySymbol)m).Name != "EqualityContract")
            .Select(m => (IPropertySymbol)m);
        var props = members.Select(m => PropertyModel.Create(m, elementNamesByInterface));

        foreach (var (_, generatorDiagnostics) in props)
            diagnostics.AddRange(generatorDiagnostics);

        var properties = props.Select(p => p.propertyModel).OfType<PropertyModel>();

        return (new RecordModel(recordSymbol.Name, recordSymbol.ContainingNamespace.ToDisplayString(), recordSymbol.Name.ToLower(), fhirType.Value, properties.ToArray(), resourceName), diagnostics);
    }

    private static bool ImplementsIElementInterface(INamedTypeSymbol recordSymbol, List<FhirGeneratorDiagnostic> diagnostics)
    {
        var implements = recordSymbol.AllInterfaces.Any(i => i.ToDisplayString() == iElementInterface);
        if (!implements)
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX001(recordSymbol));

        return implements;
    }
}

