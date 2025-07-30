using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.Generator;

public enum FhirVersion
{
    R4,
    R5
}

public struct PropertyModel(string name, string type, string typeArguments, string elementName, bool enumerable, bool required, bool fhirArray, string elementInterface, bool hasDefaultConstructor, FhirVersion fromVersion, FhirVersion toVersion)
{
    private const string iEnumerableStart = "System.Collections.Generic.IEnumerable<";

    public string Name { get; } = name;
    public string Type { get; } = type;
    public string TypeArguments { get; } = typeArguments;
    public string ElementName { get; } = elementName;
    public bool Enumerable { get; } = enumerable;
    public bool Required { get; } = required;
    public bool FhirArray { get; } = fhirArray;
    public string ElementInterface { get; } = elementInterface;
    public bool HasDefaultConstructor { get; } = hasDefaultConstructor;
    public FhirVersion FromVersion { get; } = fromVersion;
    public FhirVersion ToVersion { get; } = toVersion;

    public readonly string GetGetter()
    {
        return Type switch
        {
            "System.Uri" => ".OriginalString",
            "System.DateTimeOffset" => ".ToString(\"yyyy-MM-ddTHH:mm:ss.FFFFFFFzzz\", System.Globalization.CultureInfo.InvariantCulture)",
            "System.DateOnly" => ".ToString(\"yyyy-MM-dd\", System.Globalization.CultureInfo.InvariantCulture)",
            "System.TimeOnly" => ".ToString(\"HH:mm:ss.FFFFFFF\", System.Globalization.CultureInfo.InvariantCulture)",
            _ => ""
        };
    }

    internal static (PropertyModel? propertyModel, IEnumerable<FhirGeneratorDiagnostic> generatorDiagnostics) Create(IPropertySymbol propertySymbol, Dictionary<string, PropertyInfo> elementNamesByInterface)
    {
        var isEnumerable = propertySymbol.Type.OriginalDefinition.ToDisplayString().StartsWith(iEnumerableStart);

        ITypeSymbol? enumerableType = null;
        if (isEnumerable)
            enumerableType = propertySymbol.Type;
        else if (propertySymbol.Type.ToDisplayString() != "string")
        {
            enumerableType = propertySymbol.Type.AllInterfaces.FirstOrDefault(i => i.OriginalDefinition.ToDisplayString().StartsWith(iEnumerableStart));
        }

        bool required = true;
        ITypeSymbol? type = null;
        if (enumerableType is null)
        {
            required = propertySymbol.NullableAnnotation != NullableAnnotation.Annotated;
            type = propertySymbol.Type.WithNullableAnnotation(NullableAnnotation.NotAnnotated);
        }
        else
        {
            type = ((INamedTypeSymbol)enumerableType).TypeArguments.First();
        }
        bool enumerable = enumerableType is not null;

        var propertyInfo = FhirAttributeHelper.FindElementName(type, elementNamesByInterface);
        var fhirArray = propertyInfo?.Cardinality == FhirCardinality.Multiple;
        var mandatory = propertyInfo?.Cardinality == FhirCardinality.Mandatory;

        List<FhirGeneratorDiagnostic> diagnostics = [];
        if ((propertyInfo == null) && (!FhirAttributeHelper.IgnoreProperty(propertySymbol)))
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX003(propertySymbol));
            return (null, diagnostics);
        }
        if (!required && mandatory)
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX006(propertySymbol));
            return (null, diagnostics);
        }
        if (enumerable && !fhirArray)
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX008(propertySymbol));
            return (null, diagnostics);
        }

        var typeName = type.ToDisplayString(
            new SymbolDisplayFormat(
                typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces,
                genericsOptions: SymbolDisplayGenericsOptions.None));

        var TypeArguments = type is INamedTypeSymbol namedType && namedType.IsGenericType ?
            $"<{string.Join(",", namedType.TypeArguments.Select(t => t.ToDisplayString()))}>" : string.Empty;

        var hasDefaultConstructor = type is INamedTypeSymbol namedTypeSymbol && namedTypeSymbol.Constructors.Any(c => c.Parameters.Length == 0);

        return
            (new PropertyModel(
                propertySymbol.Name,
                typeName,
                TypeArguments,
                propertyInfo?.ElementName ?? string.Empty,
                enumerable,
                required,
                fhirArray,
                propertyInfo?.ElementInterface ?? string.Empty,
                hasDefaultConstructor,
                propertyInfo?.FromVersion ?? FhirVersion.R4,
                propertyInfo?.ToVersion ?? FhirVersion.R5),
            []);
    }
}

