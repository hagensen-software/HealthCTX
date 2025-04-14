using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace HealthCTX.Generator;

public class FhirGeneratorDiagnostic(string id, string title, string message, string category, DiagnosticSeverity severity, Location location)
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
    internal static FhirGeneratorDiagnostic CreateHCTX005(INamedTypeSymbol recordSymbol, string elementInterface, string elementName) => new(
        "HCTX005",
        "Multiple properties of same FHIR interface is not allowed",
        $"FHIR record cannot have multiple properties with same FHIR interface. Interface '{elementInterface}' for element '{elementName}' is already used.",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX006(IPropertySymbol propertySymbol) => new(
        "HCTX006",
        "Mandatory property is nullable",
        $"Property {propertySymbol.ToDisplayString()} is a mandatory FHIR element and must be implemented as not nullable",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        propertySymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX007(INamedTypeSymbol recordSymbol, IEnumerable<string> missingMandatoryInterfaces) => new(
        "HCTX007",
        "Mandatory properties are missing",
        $"FHIR record {recordSymbol.ToDisplayString()} does not implement all mandatory FHIR elements. Add properties for the following interfaces: {string.Join(", ", missingMandatoryInterfaces)}",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX008(IPropertySymbol propertySymbol) => new(
        "HCTX008",
        "Enumerable type is not allowed for singular FHIR type",
        $"Property {propertySymbol.ToDisplayString()} is an enumerable type, but the FHIR element is not an array",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        propertySymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX009(INamedTypeSymbol recordSymbol, IEnumerable<string> duplicateInterfaces) => new(
        "HCTX009",
        "Duplicate implementation of property interfaces",
        $"FHIR record {recordSymbol.ToDisplayString()} has multiple properties implementing the same FHIR interface. The following interfaces are duplicated: {string.Join(", ", duplicateInterfaces)}",
        "FhirGenerator",
        DiagnosticSeverity.Error,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);
    internal static FhirGeneratorDiagnostic CreateHCTX010(INamedTypeSymbol recordSymbol, IEnumerable<string> missingMandatoryInterfaces) => new(
        "HCTX010",
        "Mandatory properties are missing",
        $"FHIR record {recordSymbol.ToDisplayString()} does not implement all mandatory FHIR elements for some versions of HL7 Fhir. Add properties for the following interfaces: {string.Join(", ", missingMandatoryInterfaces)}",
        "FhirGenerator",
        DiagnosticSeverity.Warning,
        recordSymbol.Locations.FirstOrDefault() ?? Location.None);

    public string Id { get; } = id;
    public string Title { get; } = title;
    public string Message { get; } = message;
    public string Category { get; } = category;
    public DiagnosticSeverity Severity { get; } = severity;
    public Location Location { get; } = location;
}