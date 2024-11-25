using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.CSharpToFhirModel;

public struct RecordModel(string recordName, string recordNamespace, string recordInstanceName, string recordElementName, PropertyModel[] properties)
{
    private const string fhirElementAttribute = "HealthCTX.Domain.Framework.Attributes.FhirElementAttribute";
    private const string fhirPrimitiveAttribute = "HealthCTX.Domain.Framework.Attributes.FhirPrimitiveAttribute";

    private static readonly List<string> interfaceNames = new()
    {
        "HealthCTX.Domain.Framework.Interfaces.IId",
        "HealthCTX.Domain.Framework.Interfaces.IElement",
        "HealthCTX.Domain.CodeableConcepts.Interfaces.ICode",
        "HealthCTX.Domain.CodeableConcepts.Interfaces.ICoding"
    };

    public string RecordName { get; } = recordName;
    public string RecordNamespace { get; } = recordNamespace;
    public PropertyModel[] Properties { get; } = properties;
    public string RecordInstanceName { get; } = recordInstanceName;
    public string RecordElementName { get; } = recordElementName;

    public static RecordModel Create(GeneratorSyntaxContext context)
    {
        var recordSymbol = context.SemanticModel.GetDeclaredSymbol((RecordDeclarationSyntax)context.Node);
        if (recordSymbol == null || (ImplementsInterfaces(recordSymbol).Count() == 0))
            return new RecordModel("", "", "", "", []);

        var elementName = FindElementName(recordSymbol);

        var members = recordSymbol.GetMembers().Where(m => m.Kind == SymbolKind.Property).Where(m => ((IPropertySymbol)m).Name != "EqualityContract").Select(m => (IPropertySymbol)m);
        var properties = members.Select(m => PropertyModel.Create(m));

        return new RecordModel(recordSymbol.Name, recordSymbol.ContainingNamespace.ToDisplayString(), recordSymbol.Name.ToLower(), elementName, properties.ToArray());
    }

    private static string FindElementName(INamedTypeSymbol recordSymbol)
    {
        foreach (var intf in recordSymbol.AllInterfaces)
        {
            if (!interfaceNames.Contains(intf.ToDisplayString()))
                continue;

            var attributeData = intf.GetAttributes()
                .FirstOrDefault(attr => (attr.AttributeClass?.ToDisplayString() == fhirElementAttribute)
                                        || (attr.AttributeClass?.ToDisplayString() == fhirPrimitiveAttribute));
            if (attributeData == null)
                return "error";
            var constructorArguments = attributeData.ConstructorArguments.FirstOrDefault();

            return (string?)constructorArguments.Value ?? "error";
        }
        return "error";
    }

    public bool IsPrimitiveType()
    {
        return false;
    }

    private static IEnumerable<string> ImplementsInterfaces(INamedTypeSymbol? classSymbol)
    {
        var result = new List<string>();

        while (classSymbol != null)
        {
            result.AddRange(classSymbol.AllInterfaces.Select(i => i.ToDisplayString()).Intersect(interfaceNames));
            classSymbol = classSymbol.BaseType;
        }
        return result.AsEnumerable();
    }
}
