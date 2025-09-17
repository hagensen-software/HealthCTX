using HealthCTX.Domain.Addresses;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for HL7 FHIR Location address.</para>
/// <para>The elements from <see cref="IAddress"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ILocationAddress : IAddress;
