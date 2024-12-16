using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using HealthCTX.Generator;

namespace HealthCTX.Generator.Test;

public class FhirAttributesTest
{
    [Fact]
    public void RetriveFhirAttributes()
    {
        var code = """
            using System;

            namespace HealthCTX.Domain.Framework.Interfaces
            {
                public interface IId
                {
                }
            }

            namespace HealthCTX.Domain.Framework.Attributes
            {
                public enum Cardinality
                {
                    Single,
                    Multiple
                }

                [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
                #pragma warning disable CS9113 // Parameter is unread.
                public class FhirPropertyAttribute(string Name, Type InterfaceType, Cardinality Cardinality) : Attribute;
                #pragma warning restore CS9113 // Parameter is unread.
            }

            namespace HealthCTX.Domain.CodeableConcepts.Interfaces
            {
                using HealthCTX.Domain.Framework.Interfaces;
                using HealthCTX.Domain.Framework.Attributes;

                [FhirProperty("id", typeof(IId), Cardinality.Single)]
                public interface IElement
                {
                }
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Attribute).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var interfaceDeclaration = root.DescendantNodes().OfType<InterfaceDeclarationSyntax>().First(t => t.Identifier.Text == "IElement");
        var interfaceSymbol = model.GetDeclaredSymbol(interfaceDeclaration) as INamedTypeSymbol;

        var propertiesByElementName = FhirAttributeHelper.GetApplicableProperties([interfaceSymbol!]);

        Assert.Single(propertiesByElementName);
        Assert.Equal("id", propertiesByElementName["HealthCTX.Domain.Framework.Interfaces.IId"].ElementName);
    }

    [Fact]
    public void RetriveIndirectFhirAttributes()
    {
        var code = """
            using System;

            namespace HealthCTX.Domain.Framework.Interfaces
            {
                public interface IId
                {
                }
            }

            namespace HealthCTX.Domain.Framework.Attributes
            {
                [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
                public class FhirElementAttribute() : Attribute;

                [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
                public class FhirIgnoreAttribute : Attribute;

                [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
                public class FhirPrimitiveAttribute() : Attribute;

                public enum Cardinality
                {
                    Single,
                    Multiple
                }

                [AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
                #pragma warning disable CS9113 // Parameter is unread.
                public class FhirPropertyAttribute(string Name, Type InterfaceType, Cardinality Cardinality) : Attribute;
                #pragma warning restore CS9113 // Parameter is unread.
            }

            namespace HealthCTX.Domain.Framework.Interfaces
            {
                using HealthCTX.Domain.Framework.Attributes;

                [FhirProperty("id", typeof(IId), Cardinality.Single)]
                public interface IElement
                {
                }
            }

            namespace HealthCTX.Domain.CodeableConcepts.Interfaces
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;


                [FhirPrimitive]
                public interface ICodingCode : IElement
                {
                    [FhirIgnore]
                    string Value { get; init; }
                }
            }
            
            namespace HealthCTX.Domain.Identifiers.Interfaces
            {
                using HealthCTX.Domain.CodeableConcepts.Interfaces;
            
                public interface IIdentifierUse : ICodingCode;
            }

            namespace HealthCTX.Domain.Patients.Interfaces
            {
                using HealthCTX.Domain.Framework.Attributes;
                using HealthCTX.Domain.Framework.Interfaces;
                using HealthCTX.Domain.Identifiers.Interfaces;

                [FhirElement]
                [FhirProperty("use", typeof(IIdentifierUse), Cardinality.Single)]
                public interface IPatientIdentifier : IElement;
            }

            namespace TestNamespace
            {
                using HealthCTX.Domain.Identifiers.Interfaces;
                using HealthCTX.Domain.Patients.Interfaces;

                public record TestUse(string Value) : IIdentifierUse;

                public record TestRecord(TestUse Use) : IPatientIdentifier;
            }
            """;

        var syntaxTree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
            [
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Attribute).Assembly.Location)
            ],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        var diagnositics = compilation.GetDiagnostics();
        var compileErrors = diagnositics.Count(d => d.Severity == DiagnosticSeverity.Error);
        Assert.Equal(0, compileErrors);

        var model = compilation.GetSemanticModel(syntaxTree);
        var root = syntaxTree.GetRoot();

        var interfaceDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First(t => t.Identifier.Text == "TestRecord");
        var interfaceSymbol = model.GetDeclaredSymbol(interfaceDeclaration) as INamedTypeSymbol;

        var propertiesByElementName = FhirAttributeHelper.GetApplicableProperties(interfaceSymbol?.AllInterfaces);

        //Assert.Single(propertiesByElementName);
        //Assert.Equal("use", propertiesByElementName["HealthCTX.Domain.Identifiers.Interfaces.IIdentifierUse"]);
    }
}
