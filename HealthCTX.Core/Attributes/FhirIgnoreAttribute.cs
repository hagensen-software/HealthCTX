namespace HealthCTX.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
public class FhirIgnoreAttribute : Attribute;
