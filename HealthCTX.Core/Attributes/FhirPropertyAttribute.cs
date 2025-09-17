namespace HealthCTX.Domain.Attributes;

/// <summary>
/// Specifies the cardinality of an element, indicating the number of occurrences allowed or required.
/// </summary>
public enum Cardinality
{
    /// <summary>
    /// Represents a required or mandatory value or condition.
    /// </summary>
    /// <remarks>
    /// Records must implement this property as mandatory (non-nullable).
    /// </remarks>
    Mandatory,
    /// <summary>
    /// Represents an optional value that may or may not be present.
    /// </summary>
    /// <remarks>
    /// Records may choose to omit implementation of this property, implement it as optional (nullable), or implement it as mandatory (non-nullable).
    /// </remarks>
    Optional,
    /// <summary>
    /// Represents a collection of multiple items or entities.
    /// </summary>
    /// <remarks>
    /// Records may choose to omit implementation of this property, implement it as optional (nullable), or implement it as mandatory (non-nullable) with a collection type (e.g., List, Array).
    /// </remarks>
    Multiple
}

/// <summary>
/// Fhir attribute indicating that code generation should create FHIR property handling for this property if the give interface type is present in inherited record.
/// </summary>
[AttributeUsage(AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
#pragma warning disable CS9113 // Parameter is unread.
public class FhirPropertyAttribute(string Name, Type InterfaceType, Cardinality Cardinality, FhirVersion FromVersion = FhirVersion.R4, FhirVersion ToVersion = FhirVersion.R5) : Attribute;
#pragma warning restore CS9113 // Parameter is unread.
