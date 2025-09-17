using HealthCTX.Domain.ContactPoints;

namespace HealthCTX.Domain.ExtendedContactDetails;

/// <summary>
/// <para>Interface for HL7 FHIR ExtendedContactDetail telecom.</para>
/// <para>The elements from <see cref="IContactPoint"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IExtendedContactDetailTelecom : IContactPoint;
