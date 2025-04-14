namespace HealthCTX.Domain.Attributes;

[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirResourceAttribute(string ResourceType) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
