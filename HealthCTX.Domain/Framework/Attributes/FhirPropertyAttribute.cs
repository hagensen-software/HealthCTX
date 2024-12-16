namespace HealthCTX.Domain.Framework.Attributes;

public enum Cardinality
{
    Single,
    Multiple
}

[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirPropertyAttribute(string Name, Type InterfaceType, Cardinality Cardinality) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
