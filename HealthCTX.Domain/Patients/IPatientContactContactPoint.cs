using HealthCTX.Domain.ContactPoints;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for HL7 FHIR Patient contact.telecom.</para>
/// <para>The elements from <see cref="IContactPoint"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPatientContactContactPoint : IContactPoint;
