namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should ignore this property or interface when creating FHIR resource or element implementations.
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
public class FhirIgnoreAttribute : Attribute;
