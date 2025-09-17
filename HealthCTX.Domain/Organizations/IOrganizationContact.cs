using HealthCTX.Domain.ExtendedContactDetails;

namespace HealthCTX.Domain.Organizations;

/// <summary>
/// <para>Interface for HL7 FHIR Organization contact.</para>
/// <para>The elements from <see cref="IExtendedContactDetail"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IOrganizationContact : IExtendedContactDetail;
