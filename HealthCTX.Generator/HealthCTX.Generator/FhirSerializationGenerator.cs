using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Collections.Generic;

namespace HealthCTX.Generator;

[Generator]
public class FhirSerializationGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<(RecordModel?, IEnumerable<FhirGeneratorDiagnostic>)> provider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is RecordDeclarationSyntax,
                transform: static (context, _) => RecordModel.Create(context.SemanticModel.GetDeclaredSymbol((RecordDeclarationSyntax)context.Node)));

        context.RegisterSourceOutput(provider, (context, model) =>
        {
            ReportDiagnostics(context, model.Item2);

            if (model.Item1 is null)
                return;

            RecordModel recordModel = (RecordModel)model.Item1;

            var sb = new StringBuilder();

            CSharpFhirJsonMapperHelper.StartMapperClass(recordModel, sb);
            CSharpToFhirJsonMapperHelper.AddToFhirJsonMapper(recordModel, sb);
            CSharpFromFhirJsonMapperHelper.AddFromFhirJsonMapper(recordModel, sb);
            CSharpFhirJsonMapperHelper.EndClass(sb);

            context.AddSource($"{recordModel.RecordName}FhirJsonMapper_g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        });
    }

    private static void ReportDiagnostics(SourceProductionContext context, IEnumerable<FhirGeneratorDiagnostic> diagnostics)
    {
        foreach (var fhirDiagnositic in diagnostics)
        {
            context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor(
                fhirDiagnositic.Id,
                fhirDiagnositic.Title,
                fhirDiagnositic.Message,
                fhirDiagnositic.Category,
                fhirDiagnositic.Severity,
                true), fhirDiagnositic.Location));
        }
    }
}
