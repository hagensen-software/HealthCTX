using HealthCTX.Domain.ContactPoints;

namespace HealthCTX.Domain.Organizations;

/// <summary>
/// <para>Interface for HL7 FHIR Organization telecom.</para>
/// <para>The primitive element <see cref="IContactPoint"/> is supported.</para>
/// </summary>
public interface IOrganizationTelecom : IContactPoint;
