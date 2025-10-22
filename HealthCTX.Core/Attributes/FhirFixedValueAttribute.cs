namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should implement this property as a fixed value.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirFixedValueAttribute(string name, string value) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
