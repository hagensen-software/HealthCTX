using HealthCTX.Domain.Address;

namespace HealthCTX.Domain.Organizations;

/// <summary>
/// <para>Interface for HL7 FHIR Organization address.</para>
/// <para>The elements from <see cref="IAddress"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IOrganizationAddress : IAddress;
