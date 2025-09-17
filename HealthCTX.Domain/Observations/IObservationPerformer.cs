using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation performer.</para>
/// <para>The elements from <see cref="IReference"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationPerformer : IReference;
