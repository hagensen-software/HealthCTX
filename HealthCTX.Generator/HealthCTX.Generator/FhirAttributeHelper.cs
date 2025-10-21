using Microsoft.CodeAnalysis;
using System;
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

public readonly struct PropertyInfo(string elementName, FhirCardinality fhirCardinality, INamedTypeSymbol elementInterface, string elementInterfaceName, string? fixedValue, FhirVersion fromVersion, FhirVersion toVersion)
{
    public PropertyInfo(string elementName, FhirCardinality fhirCardinality, INamedTypeSymbol elementInterface, string elementInterfaceName, string discriminatorElement, string discriminatorValue, FhirVersion fromVersion, FhirVersion toVersion)
        : this(elementName, fhirCardinality, elementInterface, elementInterfaceName, null, fromVersion, toVersion)
    {
        DiscriminatorElement = discriminatorElement;
        DiscriminatorValue = discriminatorValue;
    }

    public string ElementName { get; } = elementName;
    public FhirCardinality Cardinality { get; } = fhirCardinality;
    public INamedTypeSymbol ElementInterface { get; } = elementInterface;
    public string ElementInterfaceName { get; } = elementInterfaceName;
    public string? FixedValue { get; } = fixedValue;
    public FhirVersion FromVersion { get; } = fromVersion;
    public FhirVersion ToVersion { get; } = toVersion;
    public string? DiscriminatorElement { get; }
    public string? DiscriminatorValue { get; }
}

public class FhirAttributeHelper
{
    private const string fhirIElement = "HealthCTX.Domain.IElement";
    private const string fhirIStringPrimitive = "HealthCTX.Domain.IStringPrimitive";
    private const string fhirIUriPrimitive = "HealthCTX.Domain.IUriPrimitive";
    private const string fhirIUrlPrimitive = "HealthCTX.Domain.IUrlPrimitive";

    private const string fhirResourceAttribute = "HealthCTX.Domain.Attributes.FhirResourceAttribute";
    private const string fhirElementAttribute = "HealthCTX.Domain.Attributes.FhirElementAttribute";
    private const string fhirPrimitiveAttribute = "HealthCTX.Domain.Attributes.FhirPrimitiveAttribute";
    private const string fhirPropertyAttribute = "HealthCTX.Domain.Attributes.FhirPropertyAttribute";
    private const string fhirValueSlicingAttribute = "HealthCTX.Domain.Attributes.FhirValueSlicingAttribute";
    private const string fhirIgnoreAttribute = "HealthCTX.Domain.Attributes.FhirIgnoreAttribute";
    private const string fhirFixedValueAttribute = "HealthCTX.Domain.Attributes.FhirFixedValueAttribute";

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
                .Where(attr =>
                    (attr.AttributeClass?.ToDisplayString() == fhirPropertyAttribute) ||
                    (attr.AttributeClass?.ToDisplayString() == fhirValueSlicingAttribute) ||
                    (attr.AttributeClass?.ToDisplayString() == fhirFixedValueAttribute));

            foreach (var attribute in attributeData)
            {
                switch (attribute.AttributeClass?.ToString())
                {
                    case fhirPropertyAttribute:
                        {
                            if (attribute.ConstructorArguments.Length is < 3 or > 5)
                                continue;

                            var elementNameIndex = 0;
                            var interfaceIndex = 1;
                            var cardinalityIndex = 2;
                            var fromVersionIndex = 3;
                            var toVersionIndex = 4;

                            var elementName = ResolveElementName(attribute.ConstructorArguments[elementNameIndex].Value as string);
                            var elementInterface = (attribute.ConstructorArguments[interfaceIndex].Value as INamedTypeSymbol);
                            var elementInterfaceName = elementInterface?.ToDisplayString();
                            var cardinality = GetCardinality(attribute, cardinalityIndex);
                            var fromVersion = GetVersion(attribute, fromVersionIndex);
                            var toVersion = GetVersion(attribute, toVersionIndex);

                            if (elementName is not null && elementInterface is not null && elementInterfaceName is not null)
                            {
                                if (result.TryGetValue(elementInterfaceName, out PropertyInfo existingProperty))
                                {
                                    if (existingProperty.FixedValue is null)
                                    {
                                        diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX005(namedTypeSymbol, elementInterfaceName!, elementName!));
                                        continue;
                                    }
                                }
                                else
                                    result.Add(elementInterfaceName, new PropertyInfo(elementName, cardinality, elementInterface, elementInterfaceName, null, fromVersion, toVersion));
                            }
                        }
                        break;
                    case fhirValueSlicingAttribute:
                        {
                            if (attribute.ConstructorArguments.Length is < 4 or > 6)
                                continue;

                            var elementNameIndex = 0;
                            var discriminatorElementIndex = 1;
                            var interfaceIndex = 2;
                            var cardinalityIndex = 3;
                            var fromVersionIndex = 4;
                            var toVersionIndex = 5;

                            var elementName = ResolveElementName(attribute.ConstructorArguments[elementNameIndex].Value as string);
                            var elementInterface = (attribute.ConstructorArguments[interfaceIndex].Value as INamedTypeSymbol);
                            var cardinality = GetCardinality(attribute, cardinalityIndex);
                            var fromVersion = GetVersion(attribute, fromVersionIndex);
                            var toVersion = GetVersion(attribute, toVersionIndex);

                            if (elementName is null ||
                                elementInterface is null ||
                                attribute.ConstructorArguments[discriminatorElementIndex].Value is not string discriminatorElement)
                            {
                                continue;
                            }

                            (var propertyInfo, var slicingDiagnostics) = ProcessSlicing(
                                namedTypeSymbol,
                                discriminatorElement,
                                elementName,
                                elementInterface,
                                cardinality,
                                fromVersion,
                                toVersion);

                            if (propertyInfo.HasValue)
                                result.Add(propertyInfo.Value.ElementInterfaceName, propertyInfo.Value);

                            diagnostics.AddRange(slicingDiagnostics);
                        }
                        break;
                    case fhirFixedValueAttribute:
                        {
                            if (attribute.ConstructorArguments.Length != 2)
                                continue;

                            var elementNameIndex = 0;
                            var elementName = ResolveElementName(attribute.ConstructorArguments[elementNameIndex].Value as string);
                            if (elementName is null)
                                continue;
                            if (attribute.ConstructorArguments[1].Value is not string fixedValue)
                                continue;

                            (var propertyInfo, var fixedValueDiagnostics) = ProcessFixedValue(elementName, fixedValue, namedTypeSymbol);

                            if (propertyInfo.HasValue)
                                result.Add(propertyInfo.Value.ElementInterfaceName, propertyInfo.Value);

                            diagnostics.AddRange(fixedValueDiagnostics);
                        }
                        break;
                    default:
                        continue;
                }
            }
        }
        return result;
    }

    private static (PropertyInfo?, List<FhirGeneratorDiagnostic>) ProcessSlicing(
        INamedTypeSymbol namedTypeSymbol,
        string discriminatorElement,
        string elementName,
        INamedTypeSymbol elementInterface,
        FhirCardinality cardinality,
        FhirVersion fromVersion,
        FhirVersion toVersion)
    {
        List<FhirGeneratorDiagnostic> diagnostics = [];

        var elementInterfaceName = elementInterface.ToDisplayString();

        var allTypeInterfaces = GetInterfacesInheritingFromIElement(namedTypeSymbol,true);
        var allElementInterfaces = GetInterfacesInheritingFromIElement(elementInterface, true);
        if (!PropertyExists(elementName, allTypeInterfaces))
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX011(namedTypeSymbol, elementName));
        }
        if (GetPropertyInterfaceOrDefault(elementName, allTypeInterfaces) is INamedTypeSymbol propertyInterface)
        {
            if (!InheritsFrom(elementInterface, propertyInterface))
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX012(namedTypeSymbol, elementName));
        }
        var discriminatorValue = string.Empty;
        if (!PropertyExists(discriminatorElement, allElementInterfaces))
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX013(namedTypeSymbol, elementName, discriminatorElement!));
        }
        else
        {
            var fixedValueAttribute = GetFixedValueAttributeOrDefault(discriminatorElement, [elementInterface, .. allElementInterfaces]);
            if (fixedValueAttribute?.ConstructorArguments[1].Value is string value)
                discriminatorValue = value;
            else
            {
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX015(namedTypeSymbol, elementName, discriminatorElement));
            }
        }
        if (GetPropertyInterfaceOrDefault(discriminatorElement, allElementInterfaces) is INamedTypeSymbol discriminatorInterface)
        {
            IEnumerable<string> interfaceNames =
                [discriminatorInterface.ToDisplayString(), .. discriminatorInterface.AllInterfaces.Select(i => i.ToDisplayString())];

            if (!interfaceNames.Intersect([fhirIStringPrimitive, fhirIUriPrimitive, fhirIUrlPrimitive]).Any())
            {
                diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX014(namedTypeSymbol, elementName, discriminatorElement!));
            }
        }

        var propertyInfo = new PropertyInfo(elementName, cardinality, elementInterface, elementInterfaceName, discriminatorElement, discriminatorValue, fromVersion, toVersion);

        return (propertyInfo, diagnostics);
    }

    private static (PropertyInfo?, List<FhirGeneratorDiagnostic>) ProcessFixedValue(string elementName, string fixedValue, INamedTypeSymbol namedTypeSymbol)
    {
        List<FhirGeneratorDiagnostic> diagnostics = [];

        var allElementInterfaces = GetInterfacesInheritingFromIElement(namedTypeSymbol);
        var propertyAttribute = GetPropertyAttributeOrDefault(elementName, allElementInterfaces);

        if (propertyAttribute is null)
        {
            diagnostics.Add(FhirGeneratorDiagnostic.CreateHCTX016(namedTypeSymbol, elementName));
            return (null, diagnostics);
        }

        var elementInterface = propertyAttribute.ConstructorArguments[1].Value as INamedTypeSymbol;

        var propertyInfo = new PropertyInfo(
            elementName,
            FhirCardinality.Mandatory,
            elementInterface!,
            elementInterface!.ToDisplayString(),
            fixedValue,
            propertyAttribute.ConstructorArguments[3].Value as FhirVersion? ?? FhirVersion.R4,
            propertyAttribute.ConstructorArguments[4].Value as FhirVersion? ?? FhirVersion.R5);

        return (propertyInfo, diagnostics);
    }

    private static FhirCardinality GetCardinality(AttributeData attribute, int cardinalityIndex)
    {
        return attribute.ConstructorArguments[cardinalityIndex].Value switch
        {
            fhirCardinalityMandatory => FhirCardinality.Mandatory,
            fhirCardinalityOptional => FhirCardinality.Optional,
            fhirCardinalityMultiple => FhirCardinality.Multiple,
            _ => FhirCardinality.Mandatory
        };
    }

    private static FhirVersion GetVersion(AttributeData attribute, int fromVersionIndex)
    {
        var fromVersion = FhirVersion.R4;
        if (attribute.ConstructorArguments.Length > fromVersionIndex)
        {
            fromVersion = attribute.ConstructorArguments[fromVersionIndex].Value switch
            {
                fhirR4 => FhirVersion.R4,
                fhirR5 => FhirVersion.R5,
                _ => FhirVersion.R4
            };
        }

        return fromVersion;
    }

    private static AttributeData? GetPropertyAttributeOrDefault(string elementName, IEnumerable<INamedTypeSymbol> allElementInterfaces)
    {
        foreach (var element in allElementInterfaces)
        {
            var attributeData = element.GetAttributes()
                .Where(attr =>
                    attr.AttributeClass?.ToDisplayString() == fhirPropertyAttribute);

            var propertyAttribute = attributeData.FirstOrDefault(a => a.ConstructorArguments[0].Value as string == elementName);
            if (propertyAttribute != null)
                return propertyAttribute;
        }

        return default;
    }

    private static AttributeData? GetFixedValueAttributeOrDefault(string elementName, IEnumerable<INamedTypeSymbol> allElementInterfaces)
    {
        foreach (var element in allElementInterfaces)
        {
            var attributeData = element.GetAttributes()
                .Where(attr =>
                    attr.AttributeClass?.ToDisplayString() == fhirFixedValueAttribute);

            var propertyAttribute = attributeData.FirstOrDefault(a => a.ConstructorArguments[0].Value as string == elementName);
            if (propertyAttribute != null)
                return propertyAttribute;
        }

        return default;
    }

    private static INamedTypeSymbol? GetPropertyInterfaceOrDefault(string elementName, IEnumerable<INamedTypeSymbol> allElementInterfaces)
    {
        var propertyAttribute = GetPropertyAttributeOrDefault(elementName, allElementInterfaces);
        if (propertyAttribute is not null)
            return propertyAttribute.ConstructorArguments[1].Value as INamedTypeSymbol;

        return default;
    }

    private static bool PropertyExists(string? discriminatorElement, IEnumerable<INamedTypeSymbol> allElementInterfaces)
    {
        foreach (var element in allElementInterfaces)
        {
            var attributeData = element.GetAttributes()
                .Where(attr =>
                    attr.AttributeClass?.ToDisplayString() == fhirPropertyAttribute);

            if (attributeData.Any(a => a.ConstructorArguments[0].Value as string == discriminatorElement))
                return true;
        }
        return false;
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

        var intf = interfaces.FirstOrDefault(i => elementNamesByInterface.ContainsKey(i.ToDisplayString()));

        //var inft = interfaces.FirstOrDefault();
        if (intf == null)
            return null;

        if (elementNamesByInterface.TryGetValue(intf.ToDisplayString(), out PropertyInfo result))
            return result;
        else
            return null;
    }

    private static IEnumerable<INamedTypeSymbol> GetInterfacesInheritingFromIElement(ITypeSymbol typeSymbol, bool includeIElement = false)
    {
        var baseInterfaceSymbol = typeSymbol.AllInterfaces.Where(i => i.ToDisplayString() == fhirIElement).FirstOrDefault();
        if (baseInterfaceSymbol == null)
            return [];

        var interfaces = typeSymbol.AllInterfaces;
        var result = interfaces.Where(i => InheritsFrom(i, baseInterfaceSymbol));
        if (includeIElement)
            result = [.. result, baseInterfaceSymbol];
        return result;
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
        List<FhirType?> fhirTypes = [];

        resourceName = null;

        foreach (var intf in recordSymbol.AllInterfaces)
        {
            var attributeData = intf.GetAttributes()
                .FirstOrDefault(attr =>
                    (attr.AttributeClass?.ToDisplayString() == fhirPrimitiveAttribute)
                    || (attr.AttributeClass?.ToDisplayString() == fhirElementAttribute)
                    || (attr.AttributeClass?.ToDisplayString() == fhirResourceAttribute)
                    || (attr.AttributeClass?.ToDisplayString() == fhirIgnoreAttribute));
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
            }
            fhirTypes.Add(fhirType);
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
