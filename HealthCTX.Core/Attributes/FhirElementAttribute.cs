namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Fhir attribute indicating that code generation should create FHIR element implementation for records inheriting this interface.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
public class FhirElementAttribute() : Attribute;
