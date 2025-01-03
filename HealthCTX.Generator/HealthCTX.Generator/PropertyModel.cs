﻿using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.Generator;

public struct PropertyModel(string name, string type, string elementName, bool enumerable, bool required, bool fhirArray)
{
    private const string iEnumerableStart = "System.Collections.Generic.IEnumerable<";

    public string Name { get; } = name;
    public string Type { get; } = type;
    public string ElementName { get; } = elementName;
    public bool Enumerable { get; } = enumerable;
    public bool Required { get; } = required;
    public bool FhirArray { get; } = fhirArray;

    public readonly string GetGetter()
    {
        return Type switch
        {
            "System.Uri" => ".OriginalString",
            "System.DateTimeOffset" => ".ToString(\"yyyy-MM-ddTHH:mm:sszzz\", System.Globalization.CultureInfo.InvariantCulture)",
            "System.DateOnly" => ".ToString(\"yyyy-MM-dd\", System.Globalization.CultureInfo.InvariantCulture)",
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

        var propertyInfo = FhirAttributeHelper.FindElementName(type, elementNamesByInterface);

        List<FhirGeneratorDiagnostic> diagnostics = [];
        if ((propertyInfo == null) && (!FhirAttributeHelper.IgnoreProperty(propertySymbol)))
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX003(propertySymbol));
            return (null, diagnostics);
        }

        return (new PropertyModel(propertySymbol.Name, type.ToDisplayString(), propertyInfo?.ElementName ?? string.Empty, enumerableType is not null, required, propertyInfo?.FhirArray ?? false), []);
    }
}

