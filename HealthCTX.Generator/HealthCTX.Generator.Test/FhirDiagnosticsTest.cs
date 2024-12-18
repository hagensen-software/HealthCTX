using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HealthCTX.Generator.Test;

public class FhirDiagnosticsTest
{
    [Fact]
    public void FhirAttributeOnNonIElement_ShouldEmitError()
    {
        var code = WrapCode(
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;

                [FhirResource("SomeResource")]
                public interface ISomeResource;

                public record SomeId(string Value) : IId;

                public record SomeResource(SomeId Id) : ISomeResource;
            }
            """);

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
        var code = WrapCode(
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;

                public interface ISomeResource : IResource;

                public record SomeId(string Value) : IId;

                public record SomeResource(SomeId Id) : ISomeResource;
            }
            """);

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
        var code = WrapCode(
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;

                [FhirResource("SomeResource")]
                public interface ISomeResource : IResource;

                public record SomeId(string Value) : IId;

                public record NotMatching : IElement;

                public record SomeResource(SomeId Id, NotMatching NM) : ISomeResource;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeResource");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        var d = Assert.Single(diagnostics);
        Assert.Equal("HCTX003", d.Id);
    }


    private static void Compile(SyntaxTree syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors)
    {
        compilation = CSharpCompilation.Create("TestAssembly",
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImmutableList<>).Assembly.Location)
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

    private string WrapCode(string codeToTest) =>
        $$"""
        using System;

        namespace HealthCTX.Domain.Framework.Attributes
        {
            public enum Cardinality
            {
                Single,
                Multiple
            }

            [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)] 
            public class FhirPropertyAttribute(string Name, Type InterfaceType, Cardinality Cardinality) : Attribute;
        
            [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)] 
            public class FhirElementAttribute() : Attribute;

            [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)] 
            public class FhirPrimitiveAttribute() : Attribute;
        
            [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)] 
            public class FhirResourceAttribute(string ResourceType) : Attribute;
        
            [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
            public class FhirIgnoreAttribute : Attribute;
        }

        namespace HealthCTX.Domain.Framework.Interfaces
        {
            using HealthCTX.Domain.Framework.Attributes;

            [FhirPrimitive]
            public interface IId : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }
        
            [FhirProperty("id", typeof(IId), Cardinality.Single)]
            public interface IElement;

            public interface IResource : IElement;
        }
        
        {{codeToTest}}
        """;
}
