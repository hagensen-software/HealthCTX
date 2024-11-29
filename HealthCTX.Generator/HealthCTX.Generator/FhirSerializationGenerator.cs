using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using System.Linq;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace HealthCTX.Generator;

[Generator]
public class FhirSerializationGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<RecordModel?> provider = context.SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) => node is RecordDeclarationSyntax,
                transform: static (context, _) => RecordModel.Create(context.SemanticModel.GetDeclaredSymbol((RecordDeclarationSyntax)context.Node)))
            .Where(r => r is not null);

        context.RegisterSourceOutput(provider, (context, model) =>
        {
            if (model is null)
                return;

            RecordModel recordModel = (RecordModel)model;
            ReportDiagnostics(context, recordModel);

            var sb = new StringBuilder();

            CSharpFhirJsonMapperHelper.StartMapperClass(recordModel, sb);
            CSharpToFhirJsonMapperHelper.AddToFhirJsonMapper(recordModel, sb);
            CSharpFromFhirJsonMapperHelper.AddFromFhirJsonMapper(recordModel, sb);
            CSharpFhirJsonMapperHelper.EndClass(sb);

            context.AddSource($"{recordModel.RecordName}FhirJsonMapper_g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        });
    }

    private static void ReportDiagnostics(SourceProductionContext context, RecordModel recordModel)
    {
        foreach (var fhirDiagnositic in recordModel.Diagnostics)
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
