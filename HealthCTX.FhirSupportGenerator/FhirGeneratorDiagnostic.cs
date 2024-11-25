using Microsoft.CodeAnalysis;

namespace HealthCTX.FhirSupportGenerator;

public class FhirGeneratorDiagnostic
{
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