namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should create sliced FHIR property handling for this property if the give interface type is present in inherited record.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirValueSlicingAttribute(string ElementName, string DiscriminatorElement, Type InterfaceType, Cardinality Cardinality, FhirVersion FromVersion = FhirVersion.R4, FhirVersion ToVersion = FhirVersion.R5) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
