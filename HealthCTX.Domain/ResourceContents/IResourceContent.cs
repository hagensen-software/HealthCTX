using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.ResourceContents;

/// <summary>
/// <para>Interface for HL7 FHIR Resource elements.</para>
/// <para>Also let the record inherit from <see cref="ResourceContent{T}"/> to define how to convert the resource to and from Fhir Json representation></para>
/// </summary>
[FhirIgnore]
public interface IResourceContent : IElement;
