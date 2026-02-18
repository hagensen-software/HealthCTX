# Create FHIR Test

Create test coverage for a FHIR interface or feature in the HealthCTX library.

## Usage

```
/create-fhir-test <interface-or-feature-name> [fhir-version]
```

Examples:
- `/create-fhir-test IEncounterBusinessStatus R6`
- `/create-fhir-test IObservationOrganizer`
- `/create-fhir-test Patient.contact.additionalName R6`

## Instructions

When creating tests for FHIR interfaces, follow these patterns:

### 1. Locate the Interface

First, find the interface definition in `HealthCTX.Domain/` or `HealthCTX.Core/` to understand:
- What properties it supports (check `[FhirProperty]` attributes)
- What base interface it extends (e.g., `ICodeableConcept`, `IReference`, `IBooleanPrimitive`)
- Version constraints (`FromVersion`, `ToVersion` parameters)

### 2. Create Test Directory

Create a new directory under the appropriate test project:
- `HealthCTX.Domain.Test/HealthCTX.Domain.Test.{Resource}/{TestName}/`
- Or for Patient/Organization: `HealthCTX.Domain.Test/{Resource}s/{TestName}/`

### 3. Create Records.cs

Follow these patterns for record definitions:

#### Primitive Types
```csharp
// String primitives - use Value property
public record Status(string Value) : IEncounterStatus;
public record Name(string Value) : ILocationName;

// Boolean primitives
public record Active(bool Value) : IPatientActive;
public record Organizer(bool Value) : IObservationOrganizer;

// DateTime primitives
public record EffectiveDate(DateTimeOffset Value) : IEncounterBusinessStatusEffectiveDate;
public record BirthDate(DateOnly Value) : IPatientBirthDate;

// Time primitives
public record StartTime(TimeOnly Value) : IAvailabilityAvailableStartTime;

// Uri primitives
public record System(Uri Value) : ICodingSystem;
```

#### CodeableConcept Types
**IMPORTANT**: Never use raw strings for coding properties. Always create proper sub-records:

```csharp
// WRONG - will cause HCTX003 error
public record MyCoding(string Code) : ICodeableConceptCoding;

// CORRECT - use ICodingCode interface
public record MyCodingCode(string Value) : ICodingCode;
public record MyCoding(MyCodingCode Code) : ICodeableConceptCoding;
public record MyCodeableConcept(ImmutableList<MyCoding> Codings) : IMyCodeableConcept;
```

#### Reference Types
```csharp
public record ReferenceValue(string Value) : IReferenceReference;
public record MyReference(ReferenceValue Reference) : IMyReference;
```

#### CodeableReference Types (R5+)
```csharp
// Concept part (CodeableConcept)
public record ConceptCodingCode(string Value) : ICodingCode;
public record ConceptCoding(ConceptCodingCode Code) : ICodeableConceptCoding;
public record MyConcept(ImmutableList<ConceptCoding> Codings) : ICodeableReferenceConcept;

// Reference part
public record RefValue(string Value) : IReferenceReference;
public record MyRef(RefValue Reference) : ICodeableReferenceReference;

// Combined CodeableReference
public record MyCodeableReference(
    MyConcept? Concept,
    MyRef? Reference) : IMyCodeableReference;
```

#### Cardinality
```csharp
// Required [1..1] - non-nullable
public record Resource(RequiredProp Required) : IResource;

// Optional [0..1] - nullable with ?
public record Resource(OptionalProp? Optional) : IResource;

// Multiple [0..*] - use ImmutableList<T>
public record Resource(ImmutableList<MultipleProp> Items) : IResource;
```

### 4. Create Test.cs

Follow this template:

```csharp
using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.{Resource}.{TestName};

public class Test
{
    [Fact]
    public void {Resource}_ToFhirJson{Version}GeneratesJsonString()
    {
        // Arrange - create the record
        var resource = new {Resource}(...);

        // Act - serialize to JSON
        (var jsonString, var outcomes) = {Resource}FhirJsonMapper.ToFhirJsonString(
            resource, FhirVersion.{Version});

        // Assert - verify no errors and correct JSON structure
        Assert.Empty(outcomes.Issues);
        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        // Navigate JSON and assert values
        var value = root.GetProperty("propertyName").GetString();
        Assert.Equal("expectedValue", value);
    }

    [Fact]
    public void {Resource}_FromFhirJson{Version}GeneratesRecords()
    {
        // Arrange - create JSON string
        var jsonString = """
            {
                "resourceType" : "{Resource}",
                "property" : "value"
            }
            """;

        // Act - deserialize to records
        (var resource, var outcomes) = {Resource}FhirJsonMapper.To{Resource}(
            jsonString, FhirVersion.{Version});

        // Assert - verify no errors and correct record properties
        Assert.Empty(outcomes.Issues);
        Assert.NotNull(resource);
        Assert.Equal("value", resource.Property.Value);
    }
}
```

### 5. Common JSON Navigation Patterns

```csharp
// Simple property
root.GetProperty("status").GetString();

// Nested object
root.GetProperty("code").GetProperty("text").GetString();

// Array first element
root.GetProperty("coding").EnumerateArray().First().GetProperty("code").GetString();

// Check property exists
Assert.False(root.TryGetProperty("optionalProp", out _));

// Boolean
root.GetProperty("active").GetBoolean();

// Nested array in array
root.GetProperty("contact")
    .EnumerateArray().First()
    .GetProperty("role")
    .EnumerateArray().First()
    .GetProperty("coding")
    .EnumerateArray().First()
    .GetProperty("code").GetString();
```

### 6. Required Imports

```csharp
// Records.cs
using HealthCTX.Domain.{Namespace};        // Resource interfaces
using HealthCTX.Domain.CodeableConcepts;   // ICodingCode, ICodeableConceptCoding
using HealthCTX.Domain.CodeableReferences; // ICodeableReferenceConcept, ICodeableReferenceReference
using HealthCTX.Domain.References;         // IReferenceReference
using HealthCTX.Domain.Availabilities;     // IAvailability, IAvailabilityAvailable
using System.Collections.Immutable;        // ImmutableList<T>

// Test.cs
using HealthCTX.Domain.Attributes;         // FhirVersion
using System.Text.Json;                    // JsonDocument, JsonElement
```

### 7. Build and Test

After creating files:

```bash
dotnet build
dotnet test --filter "FullyQualifiedName~{TestName}"
```

## Common Errors and Solutions

| Error | Cause | Solution |
|-------|-------|----------|
| HCTX003: No element name found | Property doesn't match FhirProperty attribute | Use correct interface (e.g., `ICodingCode` not `string`) |
| CS7036: No argument given | Generated mapper can't construct record | Ensure all constructor params have matching interface properties |
| HCTX010: Missing mandatory elements | Record doesn't implement required properties | Add properties for mandatory interfaces |

## Argument

$ARGUMENTS
