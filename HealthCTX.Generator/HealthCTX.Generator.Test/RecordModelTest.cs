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

        var recordModel = RecordModel.Create(recordSymbol);

        Assert.NotNull(recordModel);
        Assert.Equal("Code", recordModel?.RecordName);
        Assert.Equal(FhirType.Primitive, recordModel?.FhirType);
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
                public record MaritalStatusCoding(MaritalStatusCode Code) : ICoding;
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

        var recordModel = RecordModel.Create(recordSymbol);

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
                public record MaritalStatusCoding(MaritalStatusCode Code) : ICoding;
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

        var recordModel = RecordModel.Create(recordSymbol);

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

                public record MaritalStatusSystem(Uri Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ISystemUri;
                public record MaritalStatusVersion(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.IVersionString;
                public record MaritalStatusCode(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICodingCode;
                public record MaritalStatusDisplay(string Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.IDisplayString;
                public record MaritalStatusUserSelected(bool Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.IUserSelectedBoolean;

                public record MaritalStatusCoding(
                    MaritalStatusSystem System,
                    MaritalStatusVersion Version,
                    MaritalStatusCode Code,
                    MaritalStatusDisplay Display,
                    MaritalStatusUserSelected UserSelected) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICoding;

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

        var recordModel = RecordModel.Create(recordSymbol);

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
                public record MaritalStatusUserSelected(bool Value) : HealthCTX.Domain.CodeableConcepts.Interfaces.IUserSelectedBoolean;

                public record MaritalStatusCoding(
                    MaritalStatusUserSelected? UserSelected) : HealthCTX.Domain.CodeableConcepts.Interfaces.ICoding;
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

        var recordModel = RecordModel.Create(recordSymbol);

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

        var recordModel = RecordModel.Create(recordSymbol);

        Assert.Equal("OutcomeIssue", recordModel?.RecordName);
        Assert.Equal(FhirType.Element, recordModel?.FhirType);
        Assert.Equal("TestAssembly.OutcomeCode", recordModel?.Properties[0].Type);
    }

    #region Helpers 
    private string WrapCode(string codeToTest) => $$"""
        using System;

        namespace HealthCTX.Domain.Framework.Attributes
        {
            [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)] 
            public class FhirPropertyAttribute(string Name, Type InterfaceType) : Attribute;
        
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
            public interface IId
            {
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
            //[FhirProperty("severity", typeof(IOutcomeSeverityCode))]
            [FhirProperty("code", typeof(IOutcomeCode))]
            //[FhirProperty("details", typeof(IOutcomeDetails))]
            //[FhirProperty("diagnostics", typeof(IOutcomeDiagnostics))]
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
            public interface ISystemUri : IElement
            {
                [FhirIgnore]
                Uri Value { get; init; }
            }

            [FhirPrimitive]
            public interface IVersionString : IElement
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
            public interface IDisplayString : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }

            [FhirPrimitive]
            public interface IUserSelectedBoolean : IElement
            {
                [FhirIgnore]
                bool Value { get; init; }
            }
                
            [FhirElement]
            [FhirProperty("system", typeof(ISystemUri))]
            [FhirProperty("version", typeof(IVersionString))]
            [FhirProperty("code", typeof(ICodingCode))]
            [FhirProperty("display", typeof(IDisplayString))]
            [FhirProperty("userSelected", typeof(IUserSelectedBoolean))]
            public interface ICoding : IElement;

            [FhirPrimitive]
            public interface ICodeableConceptText : IElement
            {
                [FhirIgnore]
                string Value { get; init; }
            }
        }

        namespace HealthCTX.Domain.Patients.Interfaces
        {
            using HealthCTX.Domain.CodeableConcepts.Interfaces;
            using HealthCTX.Domain.Framework.Attributes;
            using HealthCTX.Domain.Framework.Interfaces;
        
            [FhirElement]
            [FhirProperty("coding", typeof(ICoding))]
            [FhirProperty("text", typeof(ICodeableConceptText))]
            public interface IMaritalStatusCodeableConcept : IElement;
        
            [FhirResource("Patient")]
            [FhirProperty("maritalStatus", typeof(IMaritalStatusCodeableConcept))]
            public interface IPatient : IResource;
        }

        {{codeToTest}}
        """;
    #endregion
}
