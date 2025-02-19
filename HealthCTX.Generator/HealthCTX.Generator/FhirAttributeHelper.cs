using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HealthCTX.Generator;

public enum FhirType
{
    Resource,
    Element,
    Primitive
}

public enum FhirCardinality
{
    Mandatory,
    Optional,
    Multiple
}

public readonly struct PropertyInfo(string elementName, FhirCardinality fhirCardinality, string elementInterface, FhirVersion fromVersion, FhirVersion toVersion)
{
    public string ElementName { get; } = elementName;
    public FhirCardinality Cardinality { get; } = fhirCardinality;
    public string ElementInterface { get; } = elementInterface;
    public FhirVersion FromVersion { get; } = fromVersion;
    public FhirVersion ToVersion { get; } = toVersion;
}

public class FhirAttributeHelper
{
    private const string fhirResourceAttribute = "HealthCTX.Domain.Framework.Attributes.FhirResourceAttribute";
    private const string fhirElementAttribute = "HealthCTX.Domain.Framework.Attributes.FhirElementAttribute";
    private const string fhirPrimitiveAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPrimitiveAttribute";
    private const string fhirPropertyAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPropertyAttribute";
    private const string fhirIgnoreAttribute = "HealthCTX.Domain.Framework.Attributes.FhirIgnoreAttribute";

    private const int fhirCardinalityMandatory = 0;
    private const int fhirCardinalityOptional = 1;
    private const int fhirCardinalityMultiple = 2;

    private const int fhirR4 = 0;
    private const int fhirR5 = 1;

    public static Dictionary<string, PropertyInfo> GetApplicableProperties(IEnumerable<INamedTypeSymbol> namedTypeSymbols, List<FhirGeneratorDiagnostic> diagnostics)
    {
        var result = new Dictionary<string, PropertyInfo>();

        foreach (var namedTypeSymbol in namedTypeSymbols)
        {
            var attributeData = namedTypeSymbol.GetAttributes()
                .Where(attr => (attr.AttributeClass?.ToDisplayString() == fhirPropertyAttribute));

            foreach (var attribute in attributeData)
            {
                if (attribute.ConstructorArguments.Length is <3 or >5)
                    continue;

                var elementName = ResolveElementName(attribute.ConstructorArguments[0].Value as string);
                var elementInterface = (attribute.ConstructorArguments[1].Value as INamedTypeSymbol)?.ToDisplayString();

                var cardinality = attribute.ConstructorArguments[2].Value switch
                {
                    fhirCardinalityMandatory => FhirCardinality.Mandatory,
                    fhirCardinalityOptional => FhirCardinality.Optional,
                    fhirCardinalityMultiple => FhirCardinality.Multiple,
                    _ => FhirCardinality.Mandatory
                };

                var fromVersion = FhirVersion.R4;
                if (attribute.ConstructorArguments.Length > 3)
                {
                    fromVersion = attribute.ConstructorArguments[3].Value switch
                    {
                        fhirR4 => FhirVersion.R4,
                        fhirR5 => FhirVersion.R5,
                        _ => FhirVersion.R4
                    };
                }
                var toVersion = FhirVersion.R5;
                if (attribute.ConstructorArguments.Length > 4)
                {
                    toVersion = attribute.ConstructorArguments[4].Value switch
                    {
                        fhirR4 => FhirVersion.R4,
                        fhirR5 => FhirVersion.R5,
                        _ => FhirVersion.R5
                    };
                }

                try
                {
                    if (elementName is not null && elementInterface is not null)
                        result.Add(elementInterface, new PropertyInfo(elementName, cardinality, elementInterface, fromVersion, toVersion));
                }
                catch
                {
                    diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX005(namedTypeSymbol, elementInterface!, elementName!));
                }
            }
        }
        return result;
    }

    private static string? ResolveElementName(string? elementName)
    {
        string choicePattern = @"(\w+)\[(\w+)\]";
        Match choiceMatch = Regex.Match(elementName, choicePattern);
        if (choiceMatch.Success)
            elementName = $"{choiceMatch.Groups[1].Value}{choiceMatch.Groups[2].Value}";
        else
        {
            string referencePattern = @"(\w+)\((\w+)\)";
            Match referenceMatch = Regex.Match(elementName, referencePattern);
            if (referenceMatch.Success)
                elementName = referenceMatch.Groups[1].Value;
        }
        return elementName;
    }

    public static PropertyInfo? FindElementName(ITypeSymbol recordSymbol, Dictionary<string, PropertyInfo> elementNamesByInterface)
    {
        var interfaces = GetInterfacesInheritingFromIElement(recordSymbol);

        // TODO: Check that only one interface is returned

        var inft = interfaces.FirstOrDefault();
        if (inft == null)
            return null;

        if (elementNamesByInterface.TryGetValue(inft.ToDisplayString(), out PropertyInfo result))
            return result;
        else
            return null;
    }

    private static IEnumerable<INamedTypeSymbol> GetInterfacesInheritingFromIElement(ITypeSymbol typeSymbol)
    {
        var baseInterfaceSymbol = typeSymbol.AllInterfaces.Where(i => i.ToDisplayString() == "HealthCTX.Domain.Framework.Interfaces.IElement").FirstOrDefault();
        if (baseInterfaceSymbol == null)
            return [];

        var interfaces = typeSymbol.Interfaces;
        return interfaces.Where(i => InheritsFrom(i, baseInterfaceSymbol));
    }

    private static bool InheritsFrom(INamedTypeSymbol interfaceSymbol, INamedTypeSymbol baseInterfaceSymbol)
    {
        return interfaceSymbol.AllInterfaces.Any(baseInterface => SymbolEqualityComparer.Default.Equals(baseInterface, baseInterfaceSymbol));
    }

    public static bool IgnoreProperty(IPropertySymbol propertySymbol)
    {
        var interfaces = propertySymbol.ContainingType.AllInterfaces;

        foreach (var interfaceSymbol in interfaces)
        {
            var interfaceProperty = interfaceSymbol.GetMembers().OfType<IPropertySymbol>()
                .FirstOrDefault(p => SymbolEqualityComparer.Default.Equals(propertySymbol, propertySymbol.ContainingType.FindImplementationForInterfaceMember(p)));

            var attributes = interfaceProperty?.GetAttributes();
            if ((attributes is not null) && attributes.Value.Any(attr => attr.AttributeClass?.ToDisplayString() == fhirIgnoreAttribute))
                return true;
        }
        return false;
    }

    public static FhirType? GetFhirType(INamedTypeSymbol recordSymbol, List<FhirGeneratorDiagnostic> diagnostics, out string? resourceName)
    {
        List<FhirType> fhirTypes = [];

        resourceName = null;

        foreach (var intf in recordSymbol.AllInterfaces)
        {
            var attributeData = intf.GetAttributes()
                .FirstOrDefault(attr =>
                    (attr.AttributeClass?.ToDisplayString() == fhirPrimitiveAttribute)
                    || (attr.AttributeClass?.ToDisplayString() == fhirElementAttribute)
                    || (attr.AttributeClass?.ToDisplayString() == fhirResourceAttribute));
            if (attributeData == null)
                continue;

            FhirType? fhirType = attributeData.AttributeClass?.ToDisplayString() switch
            {
                fhirPrimitiveAttribute => FhirType.Primitive,
                fhirElementAttribute => FhirType.Element,
                fhirResourceAttribute => FhirType.Resource,
                _ => null
            };

            if (fhirType.HasValue)
            {
                if (fhirType.Value == FhirType.Resource)
                    resourceName = attributeData.ConstructorArguments[0].Value as string;

                fhirTypes.Add(fhirType.Value);
            }
        }

        var fhirTypesCount = fhirTypes.Count;
        switch (fhirTypes.Count)
        {
            case 0:
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX002(recordSymbol));
                return null;
            case 1:
                return fhirTypes.FirstOrDefault();
            default:
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX004(recordSymbol));
                return null;
        }
    }
}
