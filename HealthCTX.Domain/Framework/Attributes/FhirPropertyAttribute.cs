namespace HealthCTX.Domain.Framework.Attributes;

[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirPropertyAttribute(string Name, Type InterfaceType) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
