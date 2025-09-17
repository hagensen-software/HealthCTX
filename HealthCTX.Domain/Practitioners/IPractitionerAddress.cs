using HealthCTX.Domain.Addresses;

namespace HealthCTX.Domain.Practitioners;

/// <summary>
/// <para>Interface for HL7 FHIR Practitioner address.</para>
/// <para>The elements from <see cref="IAddress"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPractitionerAddress : IAddress;
