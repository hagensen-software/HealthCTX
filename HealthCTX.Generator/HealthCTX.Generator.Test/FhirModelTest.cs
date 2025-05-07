using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using System.Reflection;
using HealthCTX.Domain;

namespace HealthCTX.Generator.Test;

public class FhirModelTest
{
    [Fact]
    public void PropertyWithChoiceOfBooleanType_ShouldAppearInRecordModel()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;

                public interface ISomeBoolean : IBooleanPrimitive;
            
                [FhirResource("SomeResource")]
                [FhirProperty("value[Boolean]", typeof(ISomeBoolean), Cardinality.Optional)]
                public interface ISomeResource : IResource;

                public record SomeBoolean(bool Value) : ISomeBoolean;

                public record SomeResource(SomeBoolean Bool) : ISomeResource;
            }
            """;

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
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;

                [FhirElement] // Not allowed - inherits FhirPrimitive
                public interface IInvalidBoolean : IBooleanPrimitive;

                public record SomeBoolean(bool Value) : IInvalidBoolean;
            }
            """;

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
    public void PropertyOfCanonical_ShouldCreateRecordModel()
    {
        var code =
            """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;

                public interface ISomeCanonical : ICanonicalPrimitive;
            
                public record SomeCanonical(Uri Value) : ISomeCanonical;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var canonicalSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeCanonical");
        (var canonical, var canonicalDiagnostics) = RecordModel.Create(canonicalSymbol);

        Assert.Empty(canonicalDiagnostics);
        Assert.Equal(FhirType.Primitive, canonical?.FhirType);
    }

    [Fact]
    public void PropertyOfDateType_ShouldCreateRecordModel()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;

                public interface ISomeDate : IDatePrimitive;
            
                public record SomeDate(DateOnly Value) : ISomeDate;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var dateSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeDate");
        (var date, var dateDiagnostics) = RecordModel.Create(dateSymbol);

        Assert.Empty(dateDiagnostics);
        Assert.Equal(FhirType.Primitive, date?.FhirType);
    }

    [Fact]
    public void PropertyOfTimeType_ShouldCreateRecordModel()
    {
        var code =
            """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;

                public interface ISomeTime : ITimePrimitive;
            
                public record SomeTime(TimeOnly Value) : ISomeTime;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var timeSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeTime");
        (var time, var timeDiagnostics) = RecordModel.Create(timeSymbol);

        Assert.Empty(timeDiagnostics);
        Assert.Equal(FhirType.Primitive, time?.FhirType);
    }

    [Fact]
    public void TypeSpecificReference_ShouldCreateRecordModel()
    {
        var code = 
            """
            namespace TestAssembly
            {
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.References;

                public interface IOtherResourceReference : IReferenceReference;
                public interface IThirdResourceReference : IReferenceReference;
                                    
                [FhirResource("SomeResource")]
                [FhirProperty("otherReference(OtherResource)", typeof(IOtherResourceReference), Cardinality.Multiple)]
                [FhirProperty("otherReference(ThirdResource)", typeof(IThirdResourceReference), Cardinality.Multiple)]
                public interface ISomeResource : IResource;

                public record OtherResourceReference(string Value) : IOtherResourceReference;
                public record ThirdResourceReference(string Value) : IThirdResourceReference;
                        
                public record SomeRecord(OtherResourceReference Other, ThirdResourceReference Third) : ISomeResource;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var someRecordSymbol = GetRecordSymbol(syntaxTree, compilation, "SomeRecord");
        (var someRecord, var dateDiagnostics) = RecordModel.Create(someRecordSymbol);

        Assert.Empty(dateDiagnostics);
        Assert.Equal("otherReference", someRecord?.Properties.First().ElementName);
        Assert.Equal("TestAssembly.IOtherResourceReference", someRecord?.Properties.First().ElementInterface);
        Assert.Equal("otherReference", someRecord?.Properties.Last().ElementName);
        Assert.Equal("TestAssembly.IThirdResourceReference", someRecord?.Properties.Last().ElementInterface);
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
