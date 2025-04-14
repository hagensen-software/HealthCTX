using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using HealthCTX.Domain;

namespace HealthCTX.Generator.Test;

public class FhirDiagnosticsTest
{
    [Fact]
    public void FhirAttributeOnNonIElement_ShouldEmitError()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;

                [FhirResource("SomeResource")]
                public interface ISomeResource;

                public record SomeId(string Value) : IId;

                public record SomeResource(SomeId Id) : ISomeResource;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeResource");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        var d = Assert.Single(diagnostics);
        Assert.Equal("HCTX001", d.Id);
    }

    [Fact]
    public void FhirAttributeMissing_ShouldEmitError()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;

                public interface ISomeResource : IResource;

                public record SomeId(string Value) : IId;

                public record SomeResource(SomeId Id) : ISomeResource;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeResource");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        var d = Assert.Single(diagnostics);
        Assert.Equal("HCTX002", d.Id);
    }

    [Fact]
    public void PropertyNotMatchingAttributeInterface_ShouldEmitError()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;

                [FhirResource("SomeResource")]
                public interface ISomeResource : IResource;

                public record SomeId(string Value) : IId;

                public record NotMatching : IElement;

                public record SomeResource(SomeId Id, NotMatching NM) : ISomeResource;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeResource");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        var d = Assert.Single(diagnostics);
        Assert.Equal("HCTX003", d.Id);
    }

    [Fact]
    public void MultiplePropertiesWithSameInterface_ShouldEmitError()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;

                [FhirResource("SomeResource")]
                public interface ISomeResource : IElement;

                public record SomeId(string Value) : IId;
                public record SomeOtherId(string Value) : IId;
            
                public record SomeResource(SomeId Id, SomeOtherId OtherId) : ISomeResource;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeResource");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        var d = Assert.Single(diagnostics);
        Assert.Equal("HCTX009", d.Id);
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
