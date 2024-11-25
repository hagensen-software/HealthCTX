using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.FhirSupportGenerator;

public enum FhirType
{
    Resource,
    Element,
    Primitive
}

public class FhirAttributeHelper
{
    private const string fhirResourceAttribute = "HealthCTX.Domain.Framework.Attributes.FhirResourceAttribute";
    private const string fhirElementAttribute = "HealthCTX.Domain.Framework.Attributes.FhirElementAttribute";
    private const string fhirPrimitiveAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPrimitiveAttribute";
    private const string fhirPropertyAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPropertyAttribute";
    private const string fhirIgnoreAttribute = "HealthCTX.Domain.Framework.Attributes.FhirIgnoreAttribute";

    public static Dictionary<string, string> GetApplicableProperties(IEnumerable<INamedTypeSymbol> namedTypeSymbols)
    {
        var result = new Dictionary<string, string>();

        foreach (var namedTypeSymbol in namedTypeSymbols)
        {
            var attributeData = namedTypeSymbol.GetAttributes()
                .Where(attr => (attr.AttributeClass?.ToDisplayString() == fhirPropertyAttribute));

            foreach (var attribute in attributeData)
            {
                if (attribute.ConstructorArguments.Length != 2)
                    continue;

                var elementName = attribute.ConstructorArguments[0].Value as string;
                var elementInterface = (attribute.ConstructorArguments[1].Value as INamedTypeSymbol)?.ToDisplayString();

                try
                {
                    if (elementName is not null && elementInterface is not null)
                        result.Add(elementInterface, elementName);
                }
                catch
                {
                    throw new System.InvalidOperationException($"Trying to add elementName '{elementName}' for interface '{elementInterface}'. Interface is already added.");
                }
            }
        }
        return result;
    }

    public static string? FindElementName(ITypeSymbol recordSymbol, Dictionary<string, string> elementNamesByInterface)
    {
        foreach (var intf in recordSymbol.AllInterfaces)
        {
            var attributeData = intf.GetAttributes()
                .FirstOrDefault(attr => (attr.AttributeClass?.ToDisplayString() == fhirElementAttribute)
                                        || (attr.AttributeClass?.ToDisplayString() == fhirPrimitiveAttribute));
            if (attributeData == null)
                return null;

            return elementNamesByInterface[intf.ToDisplayString()];
        }
        return null;
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
                return null;

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
