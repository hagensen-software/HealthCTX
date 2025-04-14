namespace HealthCTX.Domain.Attributes;

[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
public class FhirPrimitiveAttribute() : Attribute;
