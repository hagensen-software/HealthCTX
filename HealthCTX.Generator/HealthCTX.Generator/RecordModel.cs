using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.Generator;

public struct RecordModel(string recordName, string recordTypeName, string recordNamespace, string recordInstanceName, FhirType fhirType, PropertyModel[] properties, string? resourceName)
{
    private const string iElementInterface = "HealthCTX.Domain.IElement";

    public string RecordName { get; } = recordName;
    public string RecordTypeName { get; } = recordTypeName;
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

        var recordTypeName = recordSymbol.Name;
        if (recordSymbol.IsGenericType)
        {
            // If the record is generic, we do not generate serialization code for it
            return (null, []);
        }

        var implementsIElement = ImplementsIElementInterface(recordSymbol, diagnostics);
        var fhirType = FhirAttributeHelper.GetFhirType(recordSymbol, diagnostics, out var resourceName);

        if (!fhirType.HasValue || !implementsIElement)
            return (null, diagnostics);

        var elementNamesByInterface = FhirAttributeHelper.GetApplicableProperties(recordSymbol.AllInterfaces, diagnostics);

        var fixedValues = elementNamesByInterface.Values
            .Where(p => p.FixedValue is not null)
            .Select(p => PropertyModel.Create(p).PropertyModel);

        IEnumerable<IPropertySymbol> members = GetProperties(recordSymbol);
        var props = members.Select(m => PropertyModel.Create(m, elementNamesByInterface));

        var properties = props.Select(p => p.propertyModel).OfType<PropertyModel>();
        properties = [.. properties, .. fixedValues];

        // Check if same interface has been implemented multiple times
        var duplicateInterfaces = properties
            .GroupBy(p => p.ElementInterface)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);
        if (duplicateInterfaces.Any())
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX009(recordSymbol, duplicateInterfaces));

        // Check if all mandatory interfaces are implemented
        IEnumerable<string> missingMandatoryInterfaces = elementNamesByInterface.Values
                    .Where(p => p.Cardinality is FhirCardinality.Mandatory && p.FixedValue is null)
                    .Select(p => p.ElementInterfaceName)
                    .Except(properties
                        .Where(p => p.Required)
                        .Select(p => p.ElementInterface));
        if (missingMandatoryInterfaces.Any())
        {
            if (missingMandatoryInterfaces.Any(i => elementNamesByInterface[i].FromVersion == FhirVersion.R4 && elementNamesByInterface[i].ToVersion == FhirVersion.R5))
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX007(recordSymbol, missingMandatoryInterfaces));
            else
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX010(recordSymbol, missingMandatoryInterfaces));
        }

        foreach (var (_, generatorDiagnostics) in props)
            diagnostics.AddRange(generatorDiagnostics);

        return (new RecordModel(recordSymbol.Name, recordTypeName, recordSymbol.ContainingNamespace.ToDisplayString(), recordSymbol.Name.ToLower(), fhirType.Value, [.. properties], resourceName), diagnostics);
    }

    private static IEnumerable<IPropertySymbol> GetProperties(INamedTypeSymbol recordSymbol)
    {
        var current = recordSymbol;
        List<IPropertySymbol> result = [];

        while (current is not null)
        {
            var props = current.GetMembers()
                .Where(m => m.Kind == SymbolKind.Property)
                .Where(m => ((IPropertySymbol)m).Name != "EqualityContract")
                .Select(m => (IPropertySymbol)m);

            result.AddRange(props);

            current = current.BaseType;
        }

        return result;
    }

    private static bool ImplementsIElementInterface(INamedTypeSymbol recordSymbol, List<FhirGeneratorDiagnostic> diagnostics)
    {
        var implements = recordSymbol.AllInterfaces.Any(i => i.ToDisplayString() == iElementInterface);
        if (!implements)
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX001(recordSymbol));

        return implements;
    }
}

