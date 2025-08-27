using HealthCTX.Domain.Address;
using HealthCTX.Domain.ContactPoints;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for HL7 FHIR Patient contact.address.</para>
/// <para>The elements from <see cref="IAddress"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPatientContactAddress : IAddress;
