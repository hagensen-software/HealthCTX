using Microsoft.CodeAnalysis;
using System.Linq;

namespace HealthCTX.Generator;

public class FhirGeneratorDiagnostic
{
    internal static FhirGeneratorDiagnostic CreateHCTX001(INamedTypeSymbol recordSymbol) => new(
        "HCTX001",
        "Record does not implement IElement",
        $"{recordSymbol.ToDisplayString()} has a FhirResource, FhirElement, or FhirPrimitive attribute, but does not implement the IElement interface",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX002(INamedTypeSymbol recordSymbol) => new(
        "HCTX002",
        "Fhir Base Type not found",
        $"No interface for record {recordSymbol.ToDisplayString()} has a FhirResource, FhirElement, or FhirPrimitive attribute",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX003(IPropertySymbol propertySymbol) => new(
        "HCTX003",
        "Fhir Element Name not found",
        $"No element name found for property {propertySymbol.ToDisplayString()}. No interface of the containing record has a matching FhirProperty attribute.",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        propertySymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX004(INamedTypeSymbol recordSymbol) => new(
        "HCTX004",
        "Multiple Fhir Base Types found",
        $"Interfaces for record {recordSymbol.ToDisplayString()} has cannot have multiple FhirResource, FhirElement, or FhirPrimitive attributes",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);


    public FhirGeneratorDiagnostic(string id, string title, string message, string category, DiagnosticSeverity severity, Location location)
    {
        Id = id;
        Title = title;
        Message = message;
        Category = category;
        Severity = severity;
        Location = location;
    }

    public string Id { get; }
    public string Title { get; }
    public string Message { get; }
    public string Category { get; }
    public DiagnosticSeverity Severity { get; }
    public Location Location { get; }
}