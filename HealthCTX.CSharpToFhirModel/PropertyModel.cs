using Microsoft.CodeAnalysis;

namespace HealthCTX.CSharpToFhirModel;

public struct PropertyModel(string name, string elementName, InterfaceModel[] interfaces)
{
    public string Name { get; } = name;
    public string ElementName { get; } = elementName;
    public InterfaceModel[] Interfaces { get; } = interfaces;

    internal static PropertyModel Create(IPropertySymbol propertySymbol)
    {
        return new PropertyModel(propertySymbol.Name, propertySymbol.Type.ToDisplayString(), []);
    }
}
