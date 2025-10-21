using HealthCTX.Domain;
using HealthCTX.Domain.Patients;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Reflection;

namespace HealthCTX.Generator.Test;

public class FhirSlicingTest
{
    [Fact]
    public void ElementWithSlicedElement_ShouldReturnARecordModelWithBothSlices()
    {
        var code = """
            namespace TestAssembly
            {
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.HumanNames;
                using HealthCTX.Domain.Patients;
            
                [FhirFixedValue("use", "official")]
                public interface IOfficialName : IPatientHumanName;

                [FhirFixedValue("use", "nickname")]
                public interface INickname : IPatientHumanName;

                public record FamilyName(string Value) : IHumanNameFamily;
                public record GivenName(string Value) : IHumanNameGiven;
                public record OfficialName(FamilyName FamilyName, GivenName GivenName) : IOfficialName;

                public record NameText(string Value) : IHumanNameText;
                public record Nickname(NameText Text) : INickname;

                [FhirValueSlicing("name", "use", typeof(IOfficialName), Cardinality.Mandatory)]
                [FhirValueSlicing("name", "use", typeof(INickname), Cardinality.Optional)]
                public interface IMyPatient : IPatient;

                public record Patient(OfficialName Name, Nickname? Nickname) : IMyPatient;
                }
            """;


        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "Patient");

        (var recordModel, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Empty(diagnostics);
    }

    [Fact]
    public void ResourceWithSlicedElement_ShouldReturnModelWithExtension()
    {
        var code = """
            namespace TestAssembly
            {
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.Extensions;
                using HealthCTX.Domain.HumanNames;
                using HealthCTX.Domain.Patients;
            
                [FhirFixedValue("url", "http://example.org/fhir/StructureDefinition/humanname-middle")]
                public interface IHumanNameMiddle : IExtension;

                [FhirValueSlicing("extension", "url", typeof(IHumanNameMiddle), Cardinality.Optional)]
                public interface IOfficialName : IPatientHumanName;

                public record FamilyName(string Value) : IHumanNameFamily;

                public record MiddleNameText(string Value) : IStringPrimitive;
                public record MiddleName(MiddleNameText Text) : IHumanNameMiddle;

                public record GivenName(string Value) : IHumanNameGiven;
                public record OfficialName(FamilyName FamilyName, MiddleName MiddleName, GivenName GivenName) : IOfficialName;

                public interface IMyPatient : IPatient;

                public record Patient(OfficialName Name) : IMyPatient;
            }            
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "Patient");

        (var recordModel, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Empty(diagnostics);
        Assert.Equal("name", recordModel?.Properties[0].ElementName);
    }

    [Fact]
    public void ElementWithSlicedExtension_ShouldReturnARecordModelWithExtension()
    {
        var code = """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.Extensions;
                using HealthCTX.Domain.Identifiers;

                public record BooleanPrimitive(bool Value) : IBooleanPrimitive;

                [FhirFixedValue("url", "http://example.com/fhir/StructureDefinition/identifier-hidden")]            
                public interface IIdentifierHidden : IExtension;

                public record IdentifierValue(string Value) : IIdentifierValue;
                public record IdentifierHidden(BooleanPrimitive Boolean) : IIdentifierHidden;

                [FhirValueSlicing("extension", "url", typeof(IIdentifierHidden), Cardinality.Optional)]
                public interface IExtendedIdentifier : IIdentifier;
            
                public record ExtendedIdentifier(IdentifierValue Value, IdentifierHidden? Hidden) : IExtendedIdentifier;
            }            
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordPropertySymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierHidden");

        (var recordHiddenModel, var diagnosticsProperty) = RecordModel.Create(recordPropertySymbol);

        // Assert
        Assert.Empty(diagnosticsProperty);
        Assert.Collection(recordHiddenModel.Value.Properties,
            p => Assert.Null(p.FixedValue),
            p => Assert.Equal("http://example.com/fhir/StructureDefinition/identifier-hidden", p.FixedValue));

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "ExtendedIdentifier");

        (var recordModel, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Empty(diagnostics);
        Assert.Equal("value", recordModel?.Properties[0].ElementName);
        Assert.Equal("extension", recordModel?.Properties[1].ElementName);
    }

    [Fact]
    public void ElementSlicingNonexistentAttribute_ShouldReturnErrorHCTX011()
    {
        var code = """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.Extensions;
                using HealthCTX.Domain.Identifiers;

                public record ExtensionUri(Uri Value) : IUriPrimitive;

                public record BooleanPrimitive(bool Value) : IBooleanPrimitive;

                [FhirFixedValue("url", "http://example.com/fhir/StructureDefinition/identifier-hidden")]            
                public interface IIdentifierHidden : IExtension;

                public record IdentifierHidden(ExtensionUri Url, BooleanPrimitive Boolean) : IIdentifierHidden;

                [FhirValueSlicing("nonexisting", "url", typeof(IIdentifierHidden), Cardinality.Optional)]
                public interface IExtendedIdentifier : IIdentifier;
            
                public record ExtendedIdentifier(IdentifierHidden? Hidden) : IExtendedIdentifier;
            }            
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "ExtendedIdentifier");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Single(diagnostics);
        Assert.Equal("HCTX011", diagnostics.First().Id);
        Assert.Equal(DiagnosticSeverity.Error, diagnostics.First().Severity);
    }

    [Fact]
    public void ElementSlicingAttributeInheritingWrongInterface_ShouldReturnErrorHCTX012AndHCTX013()
    {
        var code = """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.Extensions;
                using HealthCTX.Domain.Identifiers;

                [FhirFixedValue("url", "http://example.com/fhir/StructureDefinition/identifier-hidden")]            
                public interface IIdentifierHidden : IId;

                public record IdentifierHidden(string Value) : IIdentifierHidden;

                [FhirValueSlicing("extension", "url", typeof(IIdentifierHidden), Cardinality.Optional)]
                public interface IExtendedIdentifier : IIdentifier;
            
                public record ExtendedIdentifier(IdentifierHidden? Hidden) : IExtendedIdentifier;
            }            
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "ExtendedIdentifier");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Equal(2, diagnostics.Count());

        var content = diagnostics.Select(d => (d.Id, d.Severity));
        Assert.Contains(("HCTX012", DiagnosticSeverity.Error), content);
        Assert.Contains(("HCTX013", DiagnosticSeverity.Error), content);
    }

    [Fact]
    public void ElementSlicingAttributeInheritingSlicingOnNonexistingElement_ShouldReturnErrorHCTX013()
    {
        var code = """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.Extensions;
                using HealthCTX.Domain.Identifiers;
            
                public record ExtensionUri(Uri Value) : IUriPrimitive;
            
                public record BooleanPrimitive(bool Value) : IBooleanPrimitive;
            
                [FhirFixedValue("url", "http://example.com/fhir/StructureDefinition/identifier-hidden")]            
                public interface IIdentifierHidden : IExtension;
            
                public record IdentifierHidden(ExtensionUri Url, BooleanPrimitive Boolean) : IIdentifierHidden;
            
                [FhirValueSlicing("extension", "noturl", typeof(IIdentifierHidden), Cardinality.Optional)]
                public interface IExtendedIdentifier : IIdentifier;
            
                public record ExtendedIdentifier(IdentifierHidden? Hidden) : IExtendedIdentifier;
            }            
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "ExtendedIdentifier");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Single(diagnostics);
        Assert.Equal("HCTX013", diagnostics.First().Id);
        Assert.Equal(DiagnosticSeverity.Error, diagnostics.First().Severity);
    }

    [Fact]
    public void ElementSlicingAttributeInheritingSlicingOnNonStringElement_ShouldReturnErrorHCTX014()
    {
        var code = """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain.Extensions;
                using HealthCTX.Domain.Identifiers;
            
                public record ExtensionUri(Uri Value) : IUriPrimitive;
            
                public record BooleanPrimitive(bool Value) : IBooleanPrimitive;
            
                [FhirFixedValue("value[Boolean]", "true")]            
                public interface IIdentifierHidden : IExtension;
            
                public record IdentifierHidden(ExtensionUri Url, BooleanPrimitive Boolean) : IIdentifierHidden;
            
                [FhirValueSlicing("extension", "value[Boolean]", typeof(IIdentifierHidden), Cardinality.Optional)]
                public interface IExtendedIdentifier : IIdentifier;
            
                public record ExtendedIdentifier(IdentifierHidden? Hidden) : IExtendedIdentifier;
            }            
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "ExtendedIdentifier");

        (_, var diagnostics) = RecordModel.Create(recordSymbol);

        // Assert
        Assert.Single(diagnostics);
        Assert.Equal("HCTX014", diagnostics.First().Id);
        Assert.Equal(DiagnosticSeverity.Error, diagnostics.First().Severity);
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
                MetadataReference.CreateFromFile(typeof(IElement).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IPatient).Assembly.Location)
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
