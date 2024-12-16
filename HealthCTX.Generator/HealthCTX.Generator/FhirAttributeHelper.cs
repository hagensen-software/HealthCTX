using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.Generator;

public enum FhirType
{
    Resource,
    Element,
    Primitive
}

public struct PropertyInfo(string elementName, bool fhirArray)
{
    public string ElementName { get; } = elementName;
    public bool FhirArray { get; } = fhirArray;
}

public class FhirAttributeHelper
{
    private const string fhirResourceAttribute = "HealthCTX.Domain.Framework.Attributes.FhirResourceAttribute";
    private const string fhirElementAttribute = "HealthCTX.Domain.Framework.Attributes.FhirElementAttribute";
    private const string fhirPrimitiveAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPrimitiveAttribute";
    private const string fhirPropertyAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPropertyAttribute";
    private const string fhirIgnoreAttribute = "HealthCTX.Domain.Framework.Attributes.FhirIgnoreAttribute";

    private const int fhirCardinalityMultiple = 1;

    public static Dictionary<string, PropertyInfo> GetApplicableProperties(IEnumerable<INamedTypeSymbol> namedTypeSymbols)
    {
        var result = new Dictionary<string, PropertyInfo>();

        foreach (var namedTypeSymbol in namedTypeSymbols)
        {
            var attributeData = namedTypeSymbol.GetAttributes()
                .Where(attr => (attr.AttributeClass?.ToDisplayString() == fhirPropertyAttribute));

            foreach (var attribute in attributeData)
            {
                if (attribute.ConstructorArguments.Length != 3)
                    continue;

                var elementName = attribute.ConstructorArguments[0].Value as string;
                var elementInterface = (attribute.ConstructorArguments[1].Value as INamedTypeSymbol)?.ToDisplayString();
                var fhirArray = ((int?)(attribute.ConstructorArguments[2].Value) == fhirCardinalityMultiple);

                try
                {
                    if (elementName is not null && elementInterface is not null)
                        result.Add(elementInterface, new PropertyInfo(elementName, fhirArray));
                }
                catch
                {
                    throw new System.InvalidOperationException($"Trying to add elementName '{elementName}' for interface '{elementInterface}'. Interface is already added.");
                }
            }
        }
        return result;
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

    public static FhirType? GetFhirType(INamedTypeSymbol recordSymbol, out string? resourceName)
    {
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

            if (fhirType.HasValue && (fhirType.Value == FhirType.Resource))
            {
                resourceName = attributeData.ConstructorArguments[0].Value as string;
            }

            return fhirType;
        }
        return null;
    }
}
