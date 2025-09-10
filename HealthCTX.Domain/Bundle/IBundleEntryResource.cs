using HealthCTX.Domain.ResourceContents;

namespace HealthCTX.Domain.Bundle;

/// <summary>
/// <para>Interface for HL7 FHIR Bundle entry.resource.</para>
/// <para>Also let the record inherit from <see cref="ResourceContent{T}"/> to define how to convert the resource to and from Fhir Json representation></para>
/// </summary>
public interface IBundleEntryResource : IResourceContent;
