using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace HealthCTX.Generator.Test;

public class FhirModelTest
{
    [Fact]
    public void PropertyWithChoiceOfBooleanType_ShouldAppearInRecordModel()
    {
        var code = WrapCode(
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;

                public interface ISomeBoolean : IBooleanPrimitive;
            
                [FhirResource("SomeResource")]
                [FhirProperty("value[Boolean]", typeof(ISomeBoolean), Cardinality.Optional)]
                public interface ISomeResource : IResource;

                public record SomeBoolean(bool Value) : ISomeBoolean;

                public record SomeResource(SomeBoolean Bool) : ISomeResource;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var booleanSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeBoolean");
        (var boolean, var booleanDiagnostics) = RecordModel.Create(booleanSymbol);

        var resourceSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeResource");
        (var resource, var resourceDiagnostics) = RecordModel.Create(resourceSymbol);

        Assert.Empty(booleanDiagnostics);
        Assert.Equal(FhirType.Primitive, boolean?.FhirType);

        Assert.Empty(resourceDiagnostics);
        Assert.Equal(FhirType.Resource, resource?.FhirType);
        var resourceProperty = Assert.Single(resource?.Properties);
        Assert.Equal("valueBoolean", resourceProperty.ElementName);
    }

    [Fact]
    public void PropertyWithMulitpleFhirTypes_ShouldGenerateErrorIssue()
    {
        var code = WrapCode(
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;

                [FhirElement] // Not allowed - inherits FhirPrimitive
                public interface IInvalidBoolean : IBooleanPrimitive;

                public record SomeBoolean(bool Value) : IInvalidBoolean;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var propertySymbol = GetRecordSymbol(syntaxTree, compilation, "SomeBoolean");
        (var property, var propertyDiagnostics) = RecordModel.Create(propertySymbol);

        var issue = Assert.Single(propertyDiagnostics);
        Assert.Equal(DiagnosticSeverity.Error, issue.Severity);
        Assert.Null(property);
    }

    [Fact]
    public void PropertyOfDateType_ShouldCreateRecordModel()
    {
        var code = WrapCode(
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Framework.Interfaces;

                public interface ISomeDate : IDatePrimitive;
            
                public record SomeDate(DateOnly Value) : ISomeDate;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var dateSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeDate");
        (var date, var dateDiagnostics) = RecordModel.Create(dateSymbol);

        Assert.Empty(dateDiagnostics);
        Assert.Equal(FhirType.Primitive, date?.FhirType);
    }

    #region Helpers
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

    private static string WrapCode(string codeToTest) =>
        $$"""
        using System;

        namespace HealthCTX.Domain.Framework.Attributes
        {
            public enum Cardinality
            {
                Mandatory,
                Optional,
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

            [FhirPrimitive]
            public interface IBooleanPrimitive : IElement
            {
                [FhirIgnore]
                bool Value { get; init; }
            }

            [FhirPrimitive]
            public interface IDatePrimitive : IElement
            {
                [FhirIgnore]
                DateOnly Value { get; init; }
            }
                
            [FhirPrimitive]
            public interface IDateTimePrimitive : IElement
            {
                [FhirIgnore]
                DateTimeOffset Value { get; init; }
            }
                
            [FhirProperty("id", typeof(IId), Cardinality.Optional)]
            public interface IElement;

            public interface IResource : IElement;
        }
        
        {{codeToTest}}
        """;
#endregion
}
