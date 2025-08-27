using HealthCTX.Domain.Identifiers;

namespace HealthCTX.Domain.Patients;

/// <summary>
/// <para>Interface for HL7 FHIR Patient identifier.</para>
/// <para>The elements from <see cref="IIdentifier"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPatientIdentifier : IIdentifier;
