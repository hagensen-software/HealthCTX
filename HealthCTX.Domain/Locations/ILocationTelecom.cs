using HealthCTX.Domain.ContactPoints;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for HL7 FHIR Location telecom.</para>
/// <para>The primitive element <see cref="IContactPoint"/> is supported.</para>
/// </summary>
public interface ILocationTelecom : IContactPoint;
