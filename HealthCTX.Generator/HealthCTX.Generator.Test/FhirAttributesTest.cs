using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using HealthCTX.Domain.Framework.Interfaces;
using System.Collections.Immutable;
using System.Reflection;

namespace HealthCTX.Generator.Test;

public class FhirAttributesTest
{
    [Fact]
    public void RetriveFhirAttributes()
    {
        var code = """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Interfaces;

                public record SomeElement : IElement;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var interfaceSymbols = GetRecordSymbol(syntaxTree, compilation, "SomeElement").AllInterfaces;

        var propertiesByElementName = FhirAttributeHelper.GetApplicableProperties(interfaceSymbols, []);

        Assert.Single(propertiesByElementName);
        Assert.Equal("id", propertiesByElementName["HealthCTX.Domain.Framework.Interfaces.IId"].ElementName);
    }

    [Fact]
    public void RetriveIndirectFhirAttributes()
    {
        var code = """
            namespace TestNamespace
            {
                using HealthCTX.Domain.Identifiers.Interfaces;

                public record TestRecord : IIdentifier;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var interfaceSymbols = GetRecordSymbol(syntaxTree, compilation, "TestRecord").AllInterfaces;

        var propertiesByElementName = FhirAttributeHelper.GetApplicableProperties(interfaceSymbols, []);

        Assert.True(propertiesByElementName.Keys.All(name => name.StartsWith("HealthCTX.Domain.Identifiers.Interfaces") || name.StartsWith("HealthCTX.Domain.Framework.Interfaces")));
        Assert.Equal("id", propertiesByElementName["HealthCTX.Domain.Framework.Interfaces.IId"].ElementName);
        Assert.Equal("use", propertiesByElementName["HealthCTX.Domain.Identifiers.Interfaces.IIdentifierUse"].ElementName);
    }

    #region Helpers 
    private static void Compile(SyntaxTree syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors)
    {
        compilation = CSharpCompilation.Create("TestAssembly",
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImmutableList<>).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
                MetadataReference.CreateFromFile(typeof(IElement).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var diagnositics = compilation.GetDiagnostics();
        compileErrors = diagnositics.Where(d => d.Severity == DiagnosticSeverity.Error);
    }

    private static INamedTypeSymbol GetRecordSymbol(SyntaxTree syntaxTree, CSharpCompilation compilation, string recordName)
    {
        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == recordName);
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;
        return recordSymbol;
    }

    private static INamedTypeSymbol GetInterfaceSymbol(SyntaxTree syntaxTree, CSharpCompilation compilation, string recordName)
    {
        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var interfaceDeclaration = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().First(t => t.Identifier.Text == recordName);
        var interfaceSymbol = model.GetDeclaredSymbol(interfaceDeclaration) as INamedTypeSymbol;
        return interfaceSymbol;
    }
    #endregion
}
