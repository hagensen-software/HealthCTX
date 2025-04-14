namespace HealthCTX.Domain.Attributes;

public enum Cardinality
{
    Mandatory,
    Optional,
    Multiple
}

[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirPropertyAttribute(string Name, Type InterfaceType, Cardinality Cardinality, FhirVersion FromVersion = FhirVersion.R4, FhirVersion ToVersion = FhirVersion.R5) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
