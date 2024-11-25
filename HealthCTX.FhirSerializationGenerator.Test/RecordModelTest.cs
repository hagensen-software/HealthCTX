using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HealthCTX.FhirSerializationGenerator.Test
{
    public class RecordModelTest
    {
        [Fact]
        public void Test1()
        {
            var code = @"
using System;

[AttributeUsage(AttributeTargets.Interface)]
public class MyCustomAttribute : Attribute
{
}

[MyCustomAttribute]
public interface IMyInterface
{
    void MyMethod();
}

public record MyRecord : IMyInterface
{
    public void MyMethod() { }
}
";

            var syntaxTree = CSharpSyntaxTree.ParseText(code);
            var compilation = CSharpCompilation.Create("TestAssembly", new[] { syntaxTree },
                new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });

            var model = compilation.GetSemanticModel(syntaxTree);
            var root = syntaxTree.GetRoot();

            // Find the record declaration
            var recordDeclaration = root.DescendantNodes().OfType<RecordDeclarationSyntax>().First();
            var recordSymbol = model.GetDeclaredSymbol(recordDeclaration) as INamedTypeSymbol;

            // Check if the record implements the interface
            var interfaceSymbol = recordSymbol.Interfaces.FirstOrDefault(i => i.Name == "IMyInterface");
            Assert.NotNull(interfaceSymbol);

            // Check if the interface has the attribute
            var hasAttribute = interfaceSymbol.GetAttributes().Any(attr => attr.AttributeClass?.Name == "MyCustomAttribute");
            Assert.True(hasAttribute);
        }
    }
}