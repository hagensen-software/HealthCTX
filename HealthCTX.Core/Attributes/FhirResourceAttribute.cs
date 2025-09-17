namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should create FHIR resource implementation for records inheriting this interface.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirResourceAttribute(string ResourceType) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
