using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for HL7 FHIR Location endpoint.</para>
/// <para>The elements from <see cref="IReference"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ILocationEndpoint : IReference;
