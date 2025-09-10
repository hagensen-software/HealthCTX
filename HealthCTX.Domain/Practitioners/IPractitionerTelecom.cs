using HealthCTX.Domain.ContactPoints;

namespace HealthCTX.Domain.Practitioners;

/// <summary>
/// <para>Interface for HL7 FHIR Practitioner telecom.</para>
/// <para>The elements from <see cref="IContactPoint"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPractitionerTelecom : IContactPoint;
