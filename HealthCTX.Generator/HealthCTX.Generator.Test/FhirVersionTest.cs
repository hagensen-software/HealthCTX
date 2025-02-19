using HealthCTX.Domain.Framework.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Reflection;

namespace HealthCTX.Generator.Test;

public class FhirVersionTest
{
    [Fact]
    public void PropertyWithNoVersions_ShouldHaveVersionFromR4ToR5()
    {
        var code =
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;

                public interface ISomeBoolean : IBooleanPrimitive;
            
                [FhirElement]
                [FhirProperty("IsSomething", typeof(ISomeBoolean), Cardinality.Optional)]
                public interface ISomeElement : IElement;

                public record SomeBoolean(bool Value) : ISomeBoolean;
                public record SomeElement(SomeBoolean Bool) : ISomeElement;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var elementSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeElement");
        (var element, var diagnostics) = RecordModel.Create(elementSymbol);

        Assert.Equal(FhirVersion.R4, element.Value.Properties.First().FromVersion);
        Assert.Equal(FhirVersion.R5, element.Value.Properties.First().ToVersion);
    }

    [Fact]
    public void PropertyWithR5FromVersion_ShouldHaveVersionFromR5ToR5()
    {
        var code =
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework;
                using HealthCTX.Domain.Framework.Interfaces;

                public interface ISomeBoolean : IBooleanPrimitive;
            
                [FhirElement]
                [FhirProperty("IsSomething", typeof(ISomeBoolean), Cardinality.Optional, FhirVersion.R5)]
                public interface ISomeElement : IElement;

                public record SomeBoolean(bool Value) : ISomeBoolean;
                public record SomeElement(SomeBoolean Bool) : ISomeElement;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var elementSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeElement");
        (var element, var diagnostics) = RecordModel.Create(elementSymbol);

        Assert.Equal(FhirVersion.R5, element.Value.Properties.First().FromVersion);
        Assert.Equal(FhirVersion.R5, element.Value.Properties.First().ToVersion);
    }

    [Fact]
    public void PropertyWithR4FromVersionAndR4ToVersion_ShouldHaveVersionFromR4ToR4()
    {
        var code =
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework;
                using HealthCTX.Domain.Framework.Interfaces;

                public interface ISomeBoolean : IBooleanPrimitive;
            
                [FhirElement]
                [FhirProperty("IsSomething", typeof(ISomeBoolean), Cardinality.Optional, FhirVersion.R4, FhirVersion.R4)]
                public interface ISomeElement : IElement;

                public record SomeBoolean(bool Value) : ISomeBoolean;
                public record SomeElement(SomeBoolean Bool) : ISomeElement;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var elementSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeElement");
        (var element, var diagnostics) = RecordModel.Create(elementSymbol);

        Assert.Equal(FhirVersion.R4, element.Value.Properties.First().FromVersion);
        Assert.Equal(FhirVersion.R4, element.Value.Properties.First().ToVersion);
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
    #endregion
}
