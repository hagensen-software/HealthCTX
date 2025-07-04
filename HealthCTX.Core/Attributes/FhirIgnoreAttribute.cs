namespace HealthCTX.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
public class FhirIgnoreAttribute : Attribute;
