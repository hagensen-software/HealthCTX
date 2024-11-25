using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using HealthCTX.FhirSupportGenerator;

namespace HealthCTX.CSharpToFhirModel.Test;

public class FhirAttributesTest
{
    [Fact]
    public void RetriveFhirAttributes()
    {
        var code = """
            using System;

            namespace HealthCTX.Domain.Framework.Interfaces
            {
                public interface IId
                {
                }
            }

            namespace HealthCTX.Domain.Framework.Attributes
            {
                public class FhirPropertyAttribute(string Name, Type InterfaceType) : Attribute;
            }

            namespace HealthCTX.Domain.CodeableConcepts.Interfaces
            {
                using HealthCTX.Domain.Framework.Interfaces;
                using HealthCTX.Domain.Framework.Attributes;

                [FhirProperty("id", typeof(IId))]
                public interface IElement
                {
                }
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Attribute).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var interfaceDeclaration = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().First(t => t.Identifier.Text == "IElement");
        var interfaceSymbol = model.GetDeclaredSymbol(interfaceDeclaration) as INamedTypeSymbol;

        var propertiesByElementName = FhirAttributeHelper.GetApplicableProperties([interfaceSymbol!]);

        Assert.Single(propertiesByElementName);
        Assert.Equal("id", propertiesByElementName["HealthCTX.Domain.Framework.Interfaces.IId"]);
    }
}
