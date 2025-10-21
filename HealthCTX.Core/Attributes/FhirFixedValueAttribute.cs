namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should implement this property as a fixed value.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
public class FhirFixedValueAttribute(string name, string value) : Attribute;
