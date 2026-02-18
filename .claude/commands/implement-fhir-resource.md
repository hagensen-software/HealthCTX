# Implement FHIR Resource

Create C# record implementations for FHIR resources using the HealthCTX library.

## Usage

```
/implement-fhir-resource <resource-type> [properties...]
```

Examples:
- `/implement-fhir-resource Patient`
- `/implement-fhir-resource Observation status code subject`
- `/implement-fhir-resource Organization with qualifications`

## Instructions

### 1. Understand the Interface

First, examine the FHIR interface in `HealthCTX.Domain/` to understand:
- Required properties (`Cardinality.Mandatory`)
- Optional properties (`Cardinality.Optional`)
- Multiple properties (`Cardinality.Multiple`)
- Version-specific properties (`FromVersion`, `ToVersion`)

### 2. Implementation Patterns

#### Basic Resource Structure

```csharp
using HealthCTX.Domain.{Resource}s;
using HealthCTX.Domain.CodeableConcepts;
using System.Collections.Immutable;

namespace MyApp.Fhir;

// Implement required primitive properties
public record PatientActive(bool Value) : IPatientActive;
public record PatientGender(string Value) : IPatientGender;
public record PatientBirthDate(DateOnly Value) : IPatientBirthDate;

// Implement the resource
public record Patient(
    PatientActive? Active,
    PatientGender? Gender,
    PatientBirthDate? BirthDate
) : IPatient;
```

#### CodeableConcept Properties

Always follow this three-level pattern:

```csharp
// 1. Coding sub-elements
public record MaritalStatusCodingSystem(Uri Value) : ICodingSystem;
public record MaritalStatusCodingCode(string Value) : ICodingCode;
public record MaritalStatusCodingDisplay(string Value) : ICodingDisplay;

// 2. Coding record
public record MaritalStatusCoding(
    MaritalStatusCodingSystem? System,
    MaritalStatusCodingCode? Code,
    MaritalStatusCodingDisplay? Display
) : ICodeableConceptCoding;

// 3. CodeableConcept record (with Codings collection)
public record PatientMaritalStatus(
    ImmutableList<MaritalStatusCoding> Codings
) : IPatientMaritalStatus;
```

#### Reference Properties

```csharp
public record ReferenceValue(string Value) : IReferenceReference;
public record ReferenceDisplay(string Value) : IReferenceDisplay;

public record PatientManagingOrganization(
    ReferenceValue Reference,
    ReferenceDisplay? Display
) : IPatientManagingOrganization;
```

#### Backbone Elements

For complex nested structures (like Patient.contact):

```csharp
// Contact name (HumanName)
public record ContactNameFamily(string Value) : IHumanNameFamily;
public record ContactNameGiven(string Value) : IHumanNameGiven;
public record ContactName(
    ContactNameFamily? Family,
    ImmutableList<ContactNameGiven> Given
) : IPatientContactHumanName;

// Contact telecom (ContactPoint)
public record ContactTelecomSystem(string Value) : IContactPointSystem;
public record ContactTelecomValue(string Value) : IContactPointValue;
public record ContactTelecom(
    ContactTelecomSystem? System,
    ContactTelecomValue? Value
) : IPatientContactContactPoint;

// Contact backbone element
public record PatientContact(
    ContactName? Name,
    ImmutableList<ContactTelecom> Telecoms
) : IPatientContact;

// Patient with contacts
public record Patient(
    ImmutableList<PatientContact> Contacts
) : IPatient;
```

### 3. Serialization

```csharp
// Serialize to JSON
var patient = new Patient(
    new PatientActive(true),
    new PatientGender("male"),
    new PatientBirthDate(new DateOnly(1990, 5, 15))
);

// Default version (R4)
(string? json, OperationOutcome outcomes) = patient.ToFhirJsonString();

// Specific version
(string? json, OperationOutcome outcomes) = patient.ToFhirJsonString(FhirVersion.R5);
(string? json, OperationOutcome outcomes) = patient.ToFhirJsonString(FhirVersion.R6);

// Check for issues
if (outcomes.Issues.Any())
{
    foreach (var issue in outcomes.Issues)
    {
        Console.WriteLine($"{issue.Severity}: {issue.Details}");
    }
}
```

### 4. Deserialization

```csharp
var jsonString = """
    {
        "resourceType": "Patient",
        "active": true,
        "gender": "male",
        "birthDate": "1990-05-15"
    }
    """;

// Deserialize using generated mapper
(Patient? patient, OperationOutcome outcomes) = PatientFhirJsonMapper.ToPatient(jsonString);

// With specific version
(Patient? patient, OperationOutcome outcomes) = PatientFhirJsonMapper.ToPatient(
    jsonString, FhirVersion.R5);

// Access properties
if (patient != null)
{
    bool? isActive = patient.Active?.Value;
    string? gender = patient.Gender?.Value;
    DateOnly? birthDate = patient.BirthDate?.Value;
}
```

### 5. Version-Specific Properties

Some properties only exist in certain FHIR versions:

```csharp
// R5+ only property (instantiatesCanonical)
public record ObservationInstantiatesCanonical(string Value) : IObservationInstantiatesCanonical;

// R6 only property (organizer)
public record ObservationOrganizer(bool Value) : IObservationOrganizer;

// The generator handles version filtering automatically
// Properties not valid for the target version are omitted from JSON
```

### 6. Choice Types

For properties with multiple type options (e.g., `deceased[Boolean]` or `deceased[DateTime]`):

```csharp
// Option 1: Boolean
public record PatientDeceasedBoolean(bool Value) : IPatientDeceasedBoolean;

// Option 2: DateTime
public record PatientDeceasedDateTime(DateTimeOffset Value) : IPatientDeceasedDateTime;

// Use only ONE in your Patient record
public record Patient(
    PatientDeceasedBoolean? DeceasedBoolean  // OR DeceasedDateTime, not both
) : IPatient;
```

### 7. Reusable Records

For commonly used types, consider creating shared records:

```csharp
// Shared/CommonCoding.cs
namespace MyApp.Fhir.Shared;

public record CodingSystem(Uri Value) : ICodingSystem;
public record CodingCode(string Value) : ICodingCode;
public record CodingDisplay(string Value) : ICodingDisplay;

public record CommonCoding(
    CodingSystem? System,
    CodingCode? Code,
    CodingDisplay? Display
) : ICodeableConceptCoding;
```

### 8. Required NuGet Packages

```xml
<PackageReference Include="Hagensen.HealthCTX" Version="1.0.0-alpha.*" />
<PackageReference Include="Hagensen.HealthCTX.Domain.Core" Version="1.0.0-alpha.*" />
<PackageReference Include="Hagensen.HealthCTX.Generator" Version="1.0.0-alpha.*" />
```

## Common Patterns Reference

| FHIR Type | C# Pattern |
|-----------|------------|
| string | `record Foo(string Value) : IFoo` |
| boolean | `record Foo(bool Value) : IFoo` |
| integer | `record Foo(int Value) : IFoo` |
| decimal | `record Foo(decimal Value) : IFoo` |
| date | `record Foo(DateOnly Value) : IFoo` |
| dateTime | `record Foo(DateTimeOffset Value) : IFoo` |
| instant | `record Foo(DateTimeOffset Value) : IFoo` |
| time | `record Foo(TimeOnly Value) : IFoo` |
| uri | `record Foo(Uri Value) : IFoo` |
| code | `record Foo(string Value) : IFoo` |
| CodeableConcept | Three-level pattern with `ImmutableList<Coding>` |
| Reference | `record Foo(ReferenceValue Reference) : IFoo` |
| [0..1] | Nullable property `Foo?` |
| [0..*] | `ImmutableList<Foo>` |
| [1..1] | Non-nullable property `Foo` |

## Argument

$ARGUMENTS
