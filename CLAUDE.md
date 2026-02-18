# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

HealthCTX is a C# library and Roslyn source generator for HL7 FHIR serialization/deserialization. It generates mapper code that converts between custom C# record-based domain models and FHIR JSON (R4, R5, and R6).

**Current version**: 1.0.0-alpha (breaking changes expected until stable release)

## Build Commands

```bash
dotnet restore                    # Restore dependencies
dotnet build                      # Build all projects
dotnet test                       # Run all tests
dotnet build --configuration Release  # Release build
```

## Test Commands

```bash
dotnet test                                           # Run all tests
dotnet test HealthCTX.Domain.Test.Patients            # Run single test project
dotnet test --filter "FullyQualifiedName~TestName"    # Run specific test
```

Test framework: xUnit. Test projects are under `HealthCTX.Domain.Test/` with one project per FHIR resource type.

## Architecture

### Core Components

- **HealthCTX.Core/** - FHIR interfaces, primitives (IStringPrimitive, IBooleanPrimitive, etc.), complex types (IHumanName, IAddress, IIdentifier, IContactPoint), and attributes (FhirResource, FhirProperty, FhirElement, FhirValueSlicing, FhirFixedValue)

- **HealthCTX.Domain/** - FHIR resource implementations (IPatient, IOrganization, IBundle, etc.) with pre-defined interfaces for common healthcare resources

- **HealthCTX.Generator/** - Roslyn IIncrementalGenerator that detects record declarations implementing FHIR interfaces and generates mapper classes with `ToFhirJson()` and `FromFhirJson()` methods

### How Code Generation Works

1. Developer creates C# records implementing FHIR interfaces (e.g., `record Patient(...) : IPatient`)
2. Generator detects these records via Roslyn analysis
3. Generator reads FhirProperty/FhirElement/FhirResource attributes from interfaces
4. Generator produces `{Namespace}_{RecordName}FhirJsonMapper_g.cs` with serialization/deserialization methods

### Key Patterns

**Primitive types**: Implement specific interfaces inheriting from base primitives (e.g., `IHumanNameText : IStringPrimitive`) with a `Value` property.

**Cardinality**:
- `Cardinality.Required` = [1..1], non-nullable
- `Cardinality.Optional` = [0..1], nullable
- `Cardinality.Multiple` = [0..*], ImmutableList<T>

**Choice types**: Use bracket syntax in FhirProperty (e.g., `deceased[Boolean]`, `deceased[DateTime]`) for union types.

**Slicing**: Use `FhirValueSlicing` attribute on interfaces to split enumerable properties based on discriminator values. Combine with `FhirFixedValue` attribute on slice interfaces.

**Version differences**: Use `FromVersion`/`ToVersion` parameters on FhirProperty to handle R4 vs R5 differences.

**Bundle resources**: Implement `IBundleEntryResource` and inherit from `ResourceContent<T>`, overriding `ToFhirJson()` and `ToResource()` for polymorphic resource handling.

## Code Style

- Use explicit types (no `var`)
- File-scoped namespaces
- Primary constructors preferred
- Use `System.Collections.Immutable` for collections
- Interfaces prefixed with `I`
- PascalCase for types, methods, properties
