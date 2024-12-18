using HealthCTX.Generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;

namespace HealthCTX.CSharpToFhirModel.Test;

public class RecordModelTest
{
    [Fact]
    public void FindRecord()
    {
        var code = WrapCode("""
            namespace TestAssembly
            {
                public record Code(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingCode;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "Code");
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.NotNull(recordModel);
        Assert.Equal("Code", recordModel?.RecordName);
        Assert.Equal(FhirType.Primitive, recordModel?.FhirType);
    }

    [Fact]
    public void HandleIdentifierType()
    {
        // Arrange
        var code = WrapCode("""
            namespace TestAssembly
            {
                using HealthCTX.Domain.CodeableConcepts.Interfaces;
                using HealthCTX.Domain.Identifiers.Interfaces;
                using HealthCTX.Domain.Patients.Interfaces;
                using HealthCTX.Domain.Period.Interfaces;

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
                    IdentifierPeriod Period) : IPatientIdentifier;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        // Act
        var identifierUseDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierUse");
        var identifierUseSymbol = model.GetDeclaredSymbol(identifierUseDeclaration) as INamedTypeSymbol;

        (var identifierUseModel, _) = RecordModel.Create(identifierUseSymbol);

        // Assert
        Assert.NotNull(identifierUseModel);
        Assert.Equal("IdentifierUse", identifierUseModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierUseModel?.FhirType);

        // Act
        var identifierCodeDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierCode");
        var identifierCodeSymbol = model.GetDeclaredSymbol(identifierCodeDeclaration) as INamedTypeSymbol;

        (var identifierCodeModel, _) = RecordModel.Create(identifierCodeSymbol);

        // Assert
        Assert.NotNull(identifierCodeModel);
        Assert.Equal("IdentifierCode", identifierCodeModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierCodeModel?.FhirType);

        // Act
        var identifierCodingDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierCoding");
        var identifierCodingSymbol = model.GetDeclaredSymbol(identifierCodingDeclaration) as INamedTypeSymbol;

        (var identifierCodingModel, _) = RecordModel.Create(identifierCodingSymbol);

        // Assert
        Assert.NotNull(identifierCodingModel);
        Assert.Equal("IdentifierCoding", identifierCodingModel?.RecordName);
        Assert.Equal(FhirType.Element, identifierCodingModel?.FhirType);

        // Act
        var identifierTextDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierText");
        var identifierTextSymbol = model.GetDeclaredSymbol(identifierTextDeclaration) as INamedTypeSymbol;

        var identifierTextModel = RecordModel.Create(identifierTextSymbol);

        // Assert
        Assert.NotNull(identifierTextModel.Item1);
        Assert.Equal("IdentifierText", identifierTextModel.Item1?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierTextModel.Item1?.FhirType);

        // Act
        var identifierTypeDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierType");
        var identifierTypeSymbol = model.GetDeclaredSymbol(identifierTypeDeclaration) as INamedTypeSymbol;

        (var identifierTypeModel, _) = RecordModel.Create(identifierTypeSymbol);

        // Assert
        Assert.NotNull(identifierTypeModel);
        Assert.Equal("IdentifierType", identifierTypeModel?.RecordName);
        Assert.Equal(FhirType.Element, identifierTypeModel?.FhirType);

        // Act
        var identifierSystemDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierSystem");
        var identifierSystemSymbol = model.GetDeclaredSymbol(identifierSystemDeclaration) as INamedTypeSymbol;

        (var identifierSystemModel, _) = RecordModel.Create(identifierSystemSymbol);

        // Assert
        Assert.NotNull(identifierSystemModel);
        Assert.Equal("IdentifierSystem", identifierSystemModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierSystemModel?.FhirType);

        // Act
        var identifierValueDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierValue");
        var identifierValueSymbol = model.GetDeclaredSymbol(identifierValueDeclaration) as INamedTypeSymbol;

        (var identifierValueModel, _) = RecordModel.Create(identifierValueSymbol);

        // Assert
        Assert.NotNull(identifierValueModel);
        Assert.Equal("IdentifierValue", identifierValueModel?.RecordName);
        Assert.Equal(FhirType.Primitive, identifierValueModel?.FhirType);

        // Act
        var periodStartDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "PeriodStart");
        var periodStartSymbol = model.GetDeclaredSymbol(periodStartDeclaration) as INamedTypeSymbol;

        (var periodStartModel, _) = RecordModel.Create(periodStartSymbol);

        // Assert
        Assert.NotNull(periodStartModel);
        Assert.Equal("PeriodStart", periodStartModel?.RecordName);
        Assert.Equal(FhirType.Primitive, periodStartModel?.FhirType);

        // Act
        var periodEndDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "PeriodEnd");
        var periodEndSymbol = model.GetDeclaredSymbol(periodEndDeclaration) as INamedTypeSymbol;

        (var periodEndModel, _) = RecordModel.Create(periodEndSymbol);

        // Assert
        Assert.NotNull(periodEndModel);
        Assert.Equal("PeriodEnd", periodEndModel?.RecordName);
        Assert.Equal(FhirType.Primitive, periodEndModel?.FhirType);

        // Act
        var identifierPeriodDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "IdentifierPeriod");
        var identifierPeriodSymbol = model.GetDeclaredSymbol(identifierPeriodDeclaration) as INamedTypeSymbol;

        (var identifierPeriodModel, _) = RecordModel.Create(identifierPeriodSymbol);

        // Assert
        Assert.NotNull(identifierPeriodModel);
        Assert.Equal("IdentifierPeriod", identifierPeriodModel?.RecordName);
        Assert.Equal(FhirType.Element, identifierPeriodModel?.FhirType);

        // Act
        var patientIdentifierDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "PatientIdentifier");
        var patientIdentifierSymbol = model.GetDeclaredSymbol(patientIdentifierDeclaration) as INamedTypeSymbol;

        (var patientIdentifierModel, _) = RecordModel.Create(patientIdentifierSymbol);

        // Assert
        Assert.NotNull(patientIdentifierModel);
        Assert.Equal("PatientIdentifier", patientIdentifierModel?.RecordName);
        Assert.Equal(FhirType.Element, patientIdentifierModel?.FhirType);
    }

    [Fact]
    public void HandleEnumerableType()
    {
        var code = WrapCode("""
            namespace TestAssembly
            {
                using HealthCTX.Domain.CodeableConcepts.Interfaces;
                using HealthCTX.Domain.Patients.Interfaces;
                using System.Collections.Generic;

                public record MaritalStatusCode(string Value) : ICodingCode;
                public record MaritalStatusCoding(MaritalStatusCode Code) : ICodeableConceptCoding;
                public record MaritalStatus(IEnumerable<MaritalStatusCoding> Coding) : IMaritalStatusCodeableConcept;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(IEnumerable<>).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "MaritalStatus");
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("MaritalStatus", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.True(recordModel?.Properties[0].Enumerable);
        Assert.Equal("TestAssembly.MaritalStatusCoding", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleImmutableListType()
    {
        var code = WrapCode("""
            namespace TestAssembly
            {
                using HealthCTX.Domain.CodeableConcepts.Interfaces;
                using HealthCTX.Domain.Patients.Interfaces;
                using System.Collections.Immutable;

                public record MaritalStatusCode(string Value) : ICodingCode;
                public record MaritalStatusCoding(MaritalStatusCode Code) : ICodeableConceptCoding;
                public record MaritalStatus(ImmutableList<MaritalStatusCoding> Coding) : IMaritalStatusCodeableConcept;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly",
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImmutableList<>).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "MaritalStatus");
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("MaritalStatus", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.True(recordModel?.Properties[0].Enumerable);
        Assert.Equal("TestAssembly.MaritalStatusCoding", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleResourceType()
    {
        var code = WrapCode("""
            namespace TestAssembly
            {
                using System.Collections.Immutable;

                public record MaritalStatusSystem(Uri Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingSystem;
                public record MaritalStatusVersion(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingVersion;
                public record MaritalStatusCode(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingCode;
                public record MaritalStatusDisplay(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingDisplay;
                public record MaritalStatusUserSelected(bool Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingUserSelected;

                public record MaritalStatusCoding(
                    MaritalStatusSystem System,
                    MaritalStatusVersion Version,
                    MaritalStatusCode Code,
                    MaritalStatusDisplay Display,
                    MaritalStatusUserSelected UserSelected) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodeableConceptCoding;

                public record MaritalStatus(ImmutableList<MaritalStatusCoding> Coding) : HealthCTX.Domain.Patients.Interfaces.IMaritalStatusCodeableConcept;

                public record Patient(MaritalStatus MaritalStatus) : HealthCTX.Domain.Patients.Interfaces.IPatient;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly",
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImmutableList<>).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "Patient");
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("Patient", recordModel?.RecordName);
        Assert.Equal(FhirType.Resource, recordModel?.FhirType);
        Assert.Equal("Patient", recordModel?.ResourceName);
        Assert.Equal("TestAssembly.MaritalStatus", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleNullableType()
    {
        var code = WrapCode("""
            namespace TestAssembly
            {
                public record MaritalStatusUserSelected(bool Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingUserSelected;

                public record MaritalStatusCoding(
                    MaritalStatusUserSelected? UserSelected) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodeableConceptCoding;
            }
            """);

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly",
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImmutableList<>).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "MaritalStatusCoding");
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("MaritalStatusCoding", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.Equal("TestAssembly.MaritalStatusUserSelected", recordModel?.Properties[0].Type);
    }

    [Fact]
    public void HandleOutcomeIssue()
    {
        var code = WrapCode("""
            namespace TestAssembly
            {
                using HealthCTX.Domain.OperationOutcomes.Interfaces;
                using System.Collections.Immutable;

                public record OutcomeCode(string Value) : IOutcomeCode;
                public record OutcomeIssue(OutcomeCode Code) : IOutcomeIssue;
            }
            """);
        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly",
            [syntaxTree],
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Uri).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ImmutableList<>).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "OutcomeIssue");
        var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

        (var recordModel, _) = RecordModel.Create(recordSymbol);

        Assert.Equal("OutcomeIssue", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.Equal("TestAssembly.OutcomeCode", recordModel?.Properties[0].Type);
    }

    #region Helpers 
    private string WrapCode(string codeToTest) => $$"""
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
        
            public interface IElement
            {
            }
            public interface IResource : IElement
            {
            }
        }

        namespace HealthCTX.Domain.OperationOutcomes.Interfaces
        {
            using HealthCTX.Domain.Framework.Attributes;
            using HealthCTX.Domain.Framework.Interfaces;
        
            [FhirPrimitive]
            public interface IOutcomeCode : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }
                
            [FhirElement]
            [FhirProperty("code", typeof(IOutcomeCode), Cardinality.Single)]
            public interface IOutcomeIssue : IElement
            {
            }
        }
        
        namespace HealthCTX.Domain.CodeableConcepts.Interfaces
        {
            using System;
            using HealthCTX.Domain.Framework.Interfaces;
            using HealthCTX.Domain.Framework.Attributes;

            [FhirPrimitive]
            public interface ICodingSystem : IElement
            {
                [FhirIgnore]
                Uri Value { get; init; }
            }

            [FhirPrimitive]
            public interface ICodingVersion : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }
                
            [FhirPrimitive]
            public interface ICodingCode : IElement
            {
                [FhirIgnore]                
                string Value { get; init; }
            }

            [FhirPrimitive]
            public interface ICodingDisplay : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }

            [FhirPrimitive]
            public interface ICodingUserSelected : IElement
            {
                [FhirIgnore]
                bool Value { get; init; }
            }
                
            [FhirElement]
            [FhirProperty("system", typeof(ICodingSystem), Cardinality.Single)]
            [FhirProperty("version", typeof(ICodingVersion), Cardinality.Single)]
            [FhirProperty("code", typeof(ICodingCode), Cardinality.Single)]
            [FhirProperty("display", typeof(ICodingDisplay), Cardinality.Single)]
            [FhirProperty("userSelected", typeof(ICodingUserSelected), Cardinality.Single)]
            public interface ICodeableConceptCoding : IElement;

            [FhirPrimitive]
            public interface ICodeableConceptText : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }
        }

        namespace HealthCTX.Domain.Period.Interfaces
        {
            using HealthCTX.Domain.Framework.Attributes;
            using HealthCTX.Domain.Framework.Interfaces;

            [FhirPrimitive]
            public interface IPeriodStart : IElement
            {
                [FhirIgnore]
                DateTimeOffset Value { get; init; }
            }

            [FhirPrimitive]
            public interface IPeriodEnd : IElement
            {
                [FhirIgnore]
                DateTimeOffset Value { get; init; }
            }
        }
        
        namespace HealthCTX.Domain.Identifiers.Interfaces
        {
            using HealthCTX.Domain.CodeableConcepts.Interfaces;
            using HealthCTX.Domain.Framework.Attributes;
            using HealthCTX.Domain.Framework.Interfaces;
            using HealthCTX.Domain.Period.Interfaces;
                
            public interface IIdentifierUse : ICodingCode;

            [FhirElement]
            [FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
            [FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Single)]
            public interface IIdentifierType : IElement;

            public interface IIdentifierSystem : ICodingSystem;

            [FhirPrimitive]
            public interface IIdentifierValue : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }

            [FhirElement]
            [FhirProperty("start", typeof(IPeriodStart), Cardinality.Single)]
            [FhirProperty("end", typeof(IPeriodEnd), Cardinality.Single)]
            public interface IIdentifierPeriod : IElement;
        }

        namespace HealthCTX.Domain.Patients.Interfaces
        {
            using HealthCTX.Domain.CodeableConcepts.Interfaces;
            using HealthCTX.Domain.Identifiers.Interfaces;
            using HealthCTX.Domain.Framework.Attributes;
            using HealthCTX.Domain.Framework.Interfaces;

            [FhirElement]
            [FhirProperty("use", typeof(IIdentifierUse), Cardinality.Single)]
            [FhirProperty("type", typeof(IIdentifierType), Cardinality.Single)]
            [FhirProperty("system", typeof(IIdentifierSystem), Cardinality.Single)]
            [FhirProperty("value", typeof(IIdentifierValue), Cardinality.Single)]
            [FhirProperty("period", typeof(IIdentifierPeriod), Cardinality.Single)]
            public interface IPatientIdentifier : IElement;
        
            [FhirElement]
            [FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple)]
            [FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Single)]
            public interface IMaritalStatusCodeableConcept : IElement;
        
            [FhirResource("Patient")]
            [FhirProperty("maritalStatus", typeof(IMaritalStatusCodeableConcept), Cardinality.Single)]
            public interface IPatient : IResource;
        }

        {{codeToTest}}
        """;
    #endregion
}
