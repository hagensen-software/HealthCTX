using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation device.</para>
/// <para>The elements from <see cref="IReference"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationDevice : IReference;
