using HealthCTX.Domain.ExtendedContactDetails;

namespace HealthCTX.Domain.Organizations;

/// <summary>
/// <para>Interface for HL7 FHIR Organization contact.</para>
/// <para>The primitive element <see cref="IExtendedContactDetail"/> is supported.</para>
/// </summary>
public interface IOrganizationContact : IExtendedContactDetail;
