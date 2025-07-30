# HealthCTX.Domain
The HealthCTX.Domain project is a C# library available through the NuGet package Hagensen.HealthCTX on nuget.org.

## Purpose
The purpose of the HealthCTX.Domain project is to provide a set of interfaces and classes that represent HL7 FHIR compliant resources.
These HL7 Fhir representation can be used to implement standardized interoperability with other healthcare application using the HL7 Fhir standard.
The package provides a set of interfaces that represent the resources defined in the HL7 Fhir standard and a code generator that allow for easy serialization and deserialization to and from FHIR JSON format.

The intension is to provide a set of interfaces that can be used to create your own healthcare FHIR compliant domain model based on C# records with only the properties needed for your application.

If you want to learn more about the HL7 Fhir standard you can visit the [HL7 Fhir website](https://www.hl7.org/fhir/).

## Getting Started

To get started with the HealthCTX.Domain project, follow these steps:

1. **Install .NET SDK**: Ensure you have the .NET 8 SDK installed on your machine. You can download it from the [official .NET website](https://dotnet.microsoft.com/download).

2. **Create a new project**: Create a new .NET project or use an existing one.

3. **Add NuGet package**: Add the Hagensen.HealthCTX package to your project.

## Your First Resource
Resources are created by implementing the interface of the resource type.
The following snippet shows how to create a Patient resource with an id and a name:

```csharp
using HealthCTX.Domain;
using HealthCTX.Domain.HumanName;
using HealthCTX.Domain.Patients;
using System.Collections.Immutable;

// Define a PatientId record that inherits from IPatientId to allow it to be used as a property in the Patient record.
public record PatientId(string Value) : IId;

// Define records for the human name components 'family' and 'given', inheriting from IHumanNameFamily and IHumanNameGiven.
public record PatientFamilyName(string Value) : IHumanNameFamily;
public record PatientGivenName(string Value) : IHumanNameGiven;

// Define a record for the human name component 'name', inheriting from IPatientHumanName.
public record PatientHumanName(PatientFamilyName Family, ImmutableList<PatientGivenName> Given) : IPatientHumanName;

// Define a Patient record that inherits from IPatient to allow it to be used as a patient resource.
// Use the defined records as properties on the Patient record.
public record Patient(PatientId Id, PatientHumanName Name) : IPatient;
```

This automatically generates the following mapper functions for the Patient resource to map it to and from Fhir Json representation.

```csharp
using HealthCTX.Domain.Attributes;

// Create a new instance of the Patient resource.
var patient = new Patient(
    new PatientId("123"),
    new PatientHumanName(
        new PatientFamilyName("Doe"),
        [new PatientGivenName("John")]));

// Convert the Patient resource to a Fhir Json representation using the geerated ToFhirJson method.
(var jsonFhirR4, var operationOutcomeToR4) = patient.ToFhirJsonString(); // Defaults to release 4 of the Fhir standard.
(var jsonFhirR5, var operationOutcomeToR5) = patient.ToFhirJsonString(FhirVersion.R5); // Specify the Fhir release version.

// Convert the Fhir Json representation back to a Patient resource using the generated FromFhirJson method.
(var patientFromJsonR4, var operationOutcomeFromR4) = PatientFhirJsonMapper.ToPatient(jsonFhirR4); // Defaults to release 4 of the Fhir standard.
(var patientFromJsonR5, var operationOutcomeFromR5) = PatientFhirJsonMapper.ToPatient(jsonFhirR5, FhirVersion.R5); // Specify the Fhir release version.
```

The ToFhirJson method converts the Patient resource to a Fhir Json representation, while the FromFhirJson method converts the Fhir Json representation back to a Patient resource.
Properties not implemented by the patient record is ignored by these methods. Errors that occurred during the conversion process are returned as an OperationOutcome record.

If you want to inspect the generated code, you can find it in your project under Dependencies - Analyzers - HealthCTX - HealthCTX.Generator.

## A Bundle Resource
Another example of a resource is a Bundle resource, which differs from the other resources in that it is a collection of other resources.
This makes the Bundle resource a bit more complex, but it is still easy to use.

Assuming you have a list of patients as defined above, you can create a Bundle resource like this:

```csharp
public record BundleType(string Value) : IBundleType;

public record EntryResource : ResourceContent<EntryResource>, IBundleEntryResource
{
    public override (JsonNode, List<OutcomeIssue>) ToFhirJson(FhirVersion fhirVersion)
    {
        return Value switch
        {
            Patient p => p.ToFhirJson(fhirVersion),
            _ => ResourceTypeNotSupported(Value)
        };
    }

    public override List<OutcomeIssue> ToResource(JsonElement jsonElement, string elementName, FhirVersion fhirVersion)
    {
        string? resourceType = GetResourceType(jsonElement);
        if (resourceType is null)
            return ResourceTypeMissing(elementName);

        (var resource, var outcomes) = resourceType switch
        {
            "Patient" => PatientFhirJsonMapper.ToPatient(jsonElement, elementName, fhirVersion),
            _ => ResourceTypeNotSupported(elementName, resourceType)
        };

        if (resource is not null)
            Value = resource;

        return outcomes;
    }
}

public record BundleEntry(
    EntryResource? Resource) : IBundleEntry;

public record Bundle(BundleType Type, ImmutableList<BundleEntry> Entries) : IBundle;
```

The record for the resources contained as entries in a bundle (here EntryResource) must implement the IBundleEntryResource interface, as this represents the entry element in the Bundle resource (defined by IBundle).

In addition to this, it must define how to convert the resource to and from Fhir Json representation.
This is done by inheriting from the abstract ResourceContent\<T\> record, which should be self-referencing to be able to Create resource entries of the correct record type.
It requires overriding the ToFhirJson and ToResource methods that are used to convert the resource to and from Fhir Json representation.
As seen from the example, the ToFhirJson can simpy switch on the resource type and call the ToFhirJson method of the record.
The ToResource method checks the resource type and call the appropriate FromFhirJson method for the resource type.
More complex rules may apply, e.g. if the bundle should support multiple profiles of the same type.

The use of a bundle record is as follows:

```csharp
var bundle = new Bundle(
    new BundleType("document"),
    [new BundleEntry(
                EntryResource.Create(
                    new Patient(
                        new PatientId("Patient/123"),
                        new PatientHumanName(
                            new PatientFamilyName("Doe"),
                            [new PatientGivenName("John")]))))]);

(var jsonString, _) = bundle.ToFhirJsonString();
(var bundleFromJson, _) = BundleFhirJsonMapper.ToBundle(jsonString);

var patientFromBundle = bundleFromJson?.Entries.First().Resource?.Value as Patient;
```

Note, that the resource in the entry is created using the Create method, that the EntryResource record inherits from ResourceContent\<T\>.
This ensures that the record entry wrapping the resource is of the correct type.

## Currently Defined Resource Types
The following resources is currently defined by the package and is ready for use as described above:

| Resource Name    | Interface         | Fhir Version |
| ---------------- | ----------------- | ------------ |
| Patient          | IPatient          | R4, R5       |
| Organization     | IOrganization     | R4, R5       |
| Practitioner     | IPractitioner     | R4, R5       |
| PractitionerRole | IPractitionerRole | R4, R5       |
| Observation      | IObservation      | R4, R5       |
| OperationOutcome | IOperationOutcome | R4, R5       |
| Location         | ILocation         | R4, R5       |
| Bundle           | IBundle           | R4, R5       |

## Defining the Fhir Domain Model
The Fhir domain model is defined by creating interfaces that represent the resources and elements in the Fhir standard.
The interfaces are used to define the properties of the resource or element and to specify the types of the properties.
This is done by decorating the interfaces with attributes that specify the name of the resource and the properties of the resource or element.

The interfaces does not contain any properties, but only attributes that specify the potential properties of the resource or element.
The generator will use these attributes to check that the records implementing the interface only contains properties that comply with the definition and generate the correct mapper class.

The most commonly used resources are defined in the HealthCTX.Domain project, but you can also create your own resources and elements by creating interfaces that represent the resources and elements in the Fhir standard.

### Defining Resources

Resources are created by implementing the interface of the resource type.

#### Take IPatient as an example.

```csharp
using HealthCTX.Domain.Attributes;
using HealthCTX.Domain;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirResource("Patient")]
[FhirProperty("identifier", typeof(IPatientIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPatientActive), Cardinality.Optional)]
[FhirProperty("name", typeof(IPatientHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPatientContactPoint), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPatientGender), Cardinality.Optional)]
[FhirProperty("birthDate", typeof(IPatientBirthDate), Cardinality.Optional)]
[FhirProperty("deceased[Boolean]", typeof(IPatientDeceasedBoolean), Cardinality.Optional)]
[FhirProperty("deceased[DateTime]", typeof(IPatientDeceasedDateTime), Cardinality.Optional)]
[FhirProperty("address", typeof(IPatientAddress), Cardinality.Multiple)]
[FhirProperty("maritalStatus", typeof(IPatientMaritalStatus), Cardinality.Optional)]
[FhirProperty("multipleBirth[Boolean]", typeof(IPatientMultipleBirthBoolean), Cardinality.Optional)]
[FhirProperty("multipleBirth[Integer]", typeof(IPatientMultipleBirthInteger), Cardinality.Optional)]
[FhirProperty("photo", typeof(IPatientPhoto), Cardinality.Multiple)]
[FhirProperty("contact", typeof(IPatientContact), Cardinality.Multiple)]
[FhirProperty("communication", typeof(IPatientCommunication), Cardinality.Multiple)]
[FhirProperty("generalPractitioner", typeof(IPatientGeneralPractitioner), Cardinality.Multiple)]
[FhirProperty("managingOrganization", typeof(IPatientManagingOrganization), Cardinality.Optional)]
[FhirProperty("link", typeof(IPatientLink), Cardinality.Multiple)]
public interface IPatient : IResource;
```

First of all the interface extends the IResource interface, which means that it is a Fhir resource and it has the FhirResource attribute to specify the name of the resource type.

The FhirProperty attribute is used to specify each of the potential properties of the resource.
   
- The first parameter is the name of the property, which is the same as the name of the property in the Fhir standard.
- The second parameter is the type of the property, which is the interface that represents the property type. This interface must inherit from IElement.
- The third parameter is the cardinality of the property

The cardinality is used to determine if the property is required, optional or multiple. The cardinality is specified as an enum with the following values:
- Required: The property is required and must be implemented - Corresponds to [1..1] in the Fhir standard
- Optional: The property is optional and may be implemented - Corresponds to [0..1] in the Fhir standard
- Multiple: The property may be implemented as a collection of values - Corresponds to [0..*] in the Fhir standard

When creating a record that implements the IPatient interface, the properties must implement an interface specified by one of the FhirProperty attributes.

- Required properties must be implemented and must not be a nullable type or enumerable
- Optional properties may be implemented and must not be enumerable - a not nullable property is allowed, which constrains it to [1..1]
- Multiple properties may be implemented and may be enumerable type or one of the above, which constrains it to [0..1] or [1..1]

The syntax with the brackets is used to specify the type of the property in the Fhir standard.

```csharp
[FhirProperty("deceased[Boolean]", typeof(IPatientDeceasedBoolean), Cardinality.Optional)]
[FhirProperty("deceased[DateTime]", typeof(IPatientDeceasedDateTime), Cardinality.Optional)]
```

This means that the property is a choice of two types, which is represented as a union type in the Fhir standard.
You can implement both properties in the record, but only one of them can be set at a time.

Beware, that the interface type of a property is used to identify the properties in the Fhir standard, so the interface types of the properties must be unique within a resource (and elements in general).

#### Take IOrganization as another example.

```csharp
using HealthCTX.Domain.Attributes;
using HealthCTX.Domain;

namespace HealthCTX.Domain.Organizations;

[FhirResource("Organization")]
[FhirProperty("identifier", typeof(IOrganizationIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IOrganizationActive), Cardinality.Optional)]
[FhirProperty("type", typeof(IOrganizationType), Cardinality.Multiple)]
[FhirProperty("name", typeof(IOrganizationName), Cardinality.Optional)]
[FhirProperty("alias", typeof(IOrganizationAlias), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IOrganizationTelecom), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("address", typeof(IOrganizationAddress), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("description", typeof(IOrganizationDescription), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("contact", typeof(IOrganizationContact), Cardinality.Multiple)]
[FhirProperty("partOf", typeof(IOrganizationPartOf), Cardinality.Optional)]
[FhirProperty("endpoint", typeof(IOrganizationEndpoint), Cardinality.Multiple)]
[FhirProperty("qualification", typeof(IOrganizationQualification), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
public interface IOrganization : IResource;
```

As you can see, the FhirProperty attribute has two additional parameters: FromVersion and ToVersion.
The FromVersion and ToVersion parameters are used to specify the version of the Fhir standard that the property is available in.
Default is that the property is available in both the R4 and R5 versions of the Fhir standard.

Settting ToVersion to R4 means that the property is only available when serializing the R4 version of the Fhir standard, and setting the FromVersion to R5 means that it is only available when serializing the R5 version.
This is used to observe the correct Fhir Json representation when serializing and deserializing the resource, depending on the version of the Fhir standard.

### Defining Elements for Properties

Elements are created by implementing the interface of the element type.

#### Take IHumanName as an example.

```csharp
using HealthCTX.Domain.Attributes;
using HealthCTX.Domain;

namespace HealthCTX.Domain.HumanName;

[FhirElement]
[FhirProperty("use", typeof(IHumanNameUse), Cardinality.Optional)]
[FhirProperty("text", typeof(IHumanNameText), Cardinality.Optional)]
[FhirProperty("family", typeof(IHumanNameFamily), Cardinality.Optional)]
[FhirProperty("given", typeof(IHumanNameGiven), Cardinality.Multiple)]
[FhirProperty("prefix", typeof(IHumanNamePrefix), Cardinality.Multiple)]
[FhirProperty("suffix", typeof(IHumanNameSuffix), Cardinality.Multiple)]
[FhirProperty("period", typeof(IHumanNamePeriod), Cardinality.Optional)]
public interface IHumanName : IElement;
```

As you can see, most of the attributes are the same as for the resources, but the FhirResource attribute is replaced by a FhirElement attribute, and it extends the IElement interface instead of the IResource interface.
This specifies that the interface represents an element from the Fhir standard that can be used in one or more resources.

### Defining Primitive Types
Primitive types are created by implementing the interface of the primitive type.

#### Take IHumanNameText as an example.

The IHumanNameText interface is a primitive type that represents the text of a human name.
The interface extends the IStringPrimitive interface, which means that it is a string type and can be used as a property in the Fhir standard.

```csharp
using HealthCTX.Domain;

namespace HealthCTX.Domain.HumanName;

public interface IHumanNameText : IStringPrimitive;
```

The IStringPrimitive interface is not used directly as this would often break the requirement that the interfaces of the properties must be unique.
It is defined like this:

```csharp
using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain;

[FhirPrimitive]
public interface IStringPrimitive : IElement
{
    [FhirIgnore]
    string Value { get; init; }
}
```

As you can see the interface is decorated with the FhirPrimitive attribute, which means that it is a primitive type.
The FhirIgnore attribute is used to specify that the Value property should not be serialized to the Fhir Json representation.
The Value property of primitive elements is used to specify the value of the primitive type and is handled as a special case by the generator.
