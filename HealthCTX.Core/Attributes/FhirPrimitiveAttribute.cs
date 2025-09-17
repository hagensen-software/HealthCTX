namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should create FHIR primitive implementation for records inheriting this interface.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
public class FhirPrimitiveAttribute() : Attribute;
