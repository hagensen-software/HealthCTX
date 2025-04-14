using HealthCTX.Domain;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Reflection;

namespace HealthCTX.Generator.Test;

public class RecordModelTest
{
    [Fact]
    public void FindRecord()
    {
        var code = """
            namespace TestAssembly
            {
                public record Code(string Value) : HealthCTX.Domain.CodeableConcepts.ICodingCode;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "Code");

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.NotNull(recordModel);
        Assert.Equal("Code", recordModel?.RecordName);
        Assert.Equal(FhirType.Primitive, recordModel?.FhirType);
    }

    [Fact]
    public void HandleIdentifierType()
    {
        // Arrange
        var code = """
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain.CodeableConcepts;
                using HealthCTX.Domain.Identifiers;
                using HealthCTX.Domain.Period;

                public record IdentifierUse(string Value) : IIdentifierUse;
                public record IdentifierCode(string Value) : ICodingCode;
                public record IdentifierCoding(IdentifierCode Code) : ICodeableConceptCoding;
                public record IdentifierText(string Value) : ICodeableConceptText;
                public record IdentifierType(IdentifierCoding Coding, IdentifierText Text) : IIdentifierType;
                public record IdentifierSystem(Uri Value) : IIdentifierSystem;
                public record IdentifierValue(string Value) : IIdentifierValue;

                public record PeriodStart(DateTimeOffset Value) : IPeriodStart;
                public record PeriodEnd(DateTimeOffset Value) : IPeriodEnd;
                public record IdentifierPeriod(
                    PeriodStart PeriodStart,
                    PeriodEnd PeriodEnd) : IIdentifierPeriod;

                public record PatientIdentifier(
                    IdentifierUse Use,
                    IdentifierType Type,
                    IdentifierSystem System,
                    IdentifierValue Value,
                    IdentifierPeriod Period) : IIdentifier;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        // Act
        var identifierUseSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierUse");
        (var identifierUseModel, _) = RecordModel.Create(identifierUseSymbol);

        // Assert
        Assert.NotNull(identifierUseModel);
        Assert.Equal("IdentifierUse", identifierUseModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierUseModel?.FhirType);

        // Act
        var identifierCodeSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierCode");
        (var identifierCodeModel, _) = RecordModel.Create(identifierCodeSymbol);

        // Assert
        Assert.NotNull(identifierCodeModel);
        Assert.Equal("IdentifierCode", identifierCodeModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierCodeModel?.FhirType);

        // Act
        var identifierCodingSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierCoding");
        (var identifierCodingModel, _) = RecordModel.Create(identifierCodingSymbol);

        // Assert
        Assert.NotNull(identifierCodingModel);
        Assert.Equal("IdentifierCoding", identifierCodingModel?.RecordName);
        Assert.Equal(FhirType.Element, identifierCodingModel?.FhirType);

        // Act
        var identifierTextSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierText");
        var identifierTextModel = RecordModel.Create(identifierTextSymbol);

        // Assert
        Assert.NotNull(identifierTextModel.Item1);
        Assert.Equal("IdentifierText", identifierTextModel.Item1?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierTextModel.Item1?.FhirType);

        // Act
        var identifierTypeSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierType");
        (var identifierTypeModel, _) = RecordModel.Create(identifierTypeSymbol);

        // Assert
        Assert.NotNull(identifierTypeModel);
        Assert.Equal("IdentifierType", identifierTypeModel?.RecordName);
        Assert.Equal(FhirType.Element, identifierTypeModel?.FhirType);

        // Act
        var identifierSystemSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierSystem");
        (var identifierSystemModel, _) = RecordModel.Create(identifierSystemSymbol);

        // Assert
        Assert.NotNull(identifierSystemModel);
        Assert.Equal("IdentifierSystem", identifierSystemModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierSystemModel?.FhirType);

        // Act
        var identifierValueSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierValue");
        (var identifierValueModel, _) = RecordModel.Create(identifierValueSymbol);

        // Assert
        Assert.NotNull(identifierValueModel);
        Assert.Equal("IdentifierValue", identifierValueModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierValueModel?.FhirType);

        // Act
        var periodStartSymbol = GetRecordSymbol(syntaxTree, compilation, "PeriodStart");
        (var periodStartModel, _) = RecordModel.Create(periodStartSymbol);

        // Assert
        Assert.NotNull(periodStartModel);
        Assert.Equal("PeriodStart", periodStartModel?.RecordName);
        Assert.Equal(FhirType.Primitive, periodStartModel?.FhirType);

        // Act
        var periodEndSymbol = GetRecordSymbol(syntaxTree, compilation, "PeriodEnd");
        (var periodEndModel, _) = RecordModel.Create(periodEndSymbol);

        // Assert
        Assert.NotNull(periodEndModel);
        Assert.Equal("PeriodEnd", periodEndModel?.RecordName);
        Assert.Equal(FhirType.Primitive, periodEndModel?.FhirType);

        // Act
        var identifierPeriodSymbol = GetRecordSymbol(syntaxTree, compilation, "IdentifierPeriod");
        (var identifierPeriodModel, _) = RecordModel.Create(identifierPeriodSymbol);

        // Assert
        Assert.NotNull(identifierPeriodModel);
        Assert.Equal("IdentifierPeriod", identifierPeriodModel?.RecordName);
        Assert.Equal(FhirType.Element, identifierPeriodModel?.FhirType);

        // Act
        var patientIdentifierSymbol = GetRecordSymbol(syntaxTree, compilation, "PatientIdentifier");
        (var patientIdentifierModel, _) = RecordModel.Create(patientIdentifierSymbol);

        // Assert
        Assert.NotNull(patientIdentifierModel);
        Assert.Equal("PatientIdentifier", patientIdentifierModel?.RecordName);
        Assert.Equal(FhirType.Element, patientIdentifierModel?.FhirType);
    }

    [Fact]
    public void HandleEnumerableType()
    {
        var code = """
            namespace TestAssembly
            {
                using HealthCTX.Domain.CodeableConcepts;
                using System.Collections.Generic;

                public record MaritalStatusCode(string Value) : ICodingCode;
                public record MaritalStatusCoding(MaritalStatusCode Code) : ICodeableConceptCoding;
                public record MaritalStatus(IEnumerable<MaritalStatusCoding> Coding) : ICodeableConcept;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "MaritalStatus");
        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("MaritalStatus", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.True(recordModel?.Properties[0].Enumerable);
        Assert.Equal("TestAssembly.MaritalStatusCoding", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleImmutableListType()
    {
        var code = """
            namespace TestAssembly
            {
                using HealthCTX.Domain.CodeableConcepts;
                using System.Collections.Immutable;

                public record MaritalStatusCode(string Value) : ICodingCode;
                public record MaritalStatusCoding(MaritalStatusCode Code) : ICodeableConceptCoding;
                public record MaritalStatus(ImmutableList<MaritalStatusCoding> Coding) : ICodeableConcept;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "MaritalStatus");
        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("MaritalStatus", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.True(recordModel?.Properties[0].Enumerable);
        Assert.Equal("TestAssembly.MaritalStatusCoding", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleResourceType()
    {
        var code = """
            namespace HealthCTX.Domain.Patients
            {
                using HealthCTX.Domain.CodeableConcepts;
                using HealthCTX.Domain.Identifiers;
                using HealthCTX.Domain.Attributes;
                using HealthCTX.Domain;
            
                [FhirElement]
                [FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
                [FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Optional)]
                public interface IMaritalStatusCodeableConcept : IElement;
            
                [FhirResource("Patient")]
                [FhirProperty("maritalStatus", typeof(IMaritalStatusCodeableConcept), Cardinality.Optional)]
                public interface IPatient : IResource;
            }
            
            namespace TestAssembly
            {
                using System;
                using HealthCTX.Domain.CodeableConcepts;
                using System.Collections.Immutable;

                public record MaritalStatusSystem(Uri Value) : HealthCTX.Domain.CodeableConcepts.ICodingSystem;
                public record MaritalStatusVersion(string Value) : HealthCTX.Domain.CodeableConcepts.ICodingVersion;
                public record MaritalStatusCode(string Value) : HealthCTX.Domain.CodeableConcepts.ICodingCode;
                public record MaritalStatusDisplay(string Value) : HealthCTX.Domain.CodeableConcepts.ICodingDisplay;
                public record MaritalStatusUserSelected(bool Value) : HealthCTX.Domain.CodeableConcepts.ICodingUserSelected;

                public record MaritalStatusCoding(
                    MaritalStatusSystem System,
                    MaritalStatusVersion Version,
                    MaritalStatusCode Code,
                    MaritalStatusDisplay Display,
                    MaritalStatusUserSelected UserSelected) : ICodeableConceptCoding;

                public record MaritalStatus(ImmutableList<MaritalStatusCoding> Coding) : HealthCTX.Domain.Patients.IMaritalStatusCodeableConcept;

                public record Patient(MaritalStatus MaritalStatus) : HealthCTX.Domain.Patients.IPatient;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "Patient");
        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("Patient", recordModel?.RecordName);
        Assert.Equal(FhirType.Resource, recordModel?.FhirType);
        Assert.Equal("Patient", recordModel?.ResourceName);
        Assert.Equal("TestAssembly.MaritalStatus", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleNullableType()
    {
        var code = """
            namespace TestAssembly
            {
                public record MaritalStatusUserSelected(bool Value) : HealthCTX.Domain.CodeableConcepts.ICodingUserSelected;

                public record MaritalStatusCoding(
                    MaritalStatusUserSelected? UserSelected) : HealthCTX.Domain.CodeableConcepts.ICodeableConceptCoding;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "MaritalStatusCoding");
        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("MaritalStatusCoding", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.Equal("TestAssembly.MaritalStatusUserSelected", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleOutcomeIssue()
    {
        var code = """
            namespace TestAssembly
            {
                using HealthCTX.Domain.OperationOutcomes;
                using System.Collections.Immutable;

                public record OutcomeCode(string Value) : IOutcomeCode;
                public record OutcomeIssue(OutcomeCode Code) : IOutcomeIssue;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        Compile(syntaxTree, out CSharpCompilation compilation, out IEnumerable<Diagnostic> compileErrors);
        Assert.Empty(compileErrors);

        var recordSymbol = GetRecordSymbol(syntaxTree, compilation, "OutcomeIssue");
        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("OutcomeIssue", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.Equal("TestAssembly.OutcomeCode", recordModel?.Properties[0].Type);
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
