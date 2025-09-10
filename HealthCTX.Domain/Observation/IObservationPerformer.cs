using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation performer.</para>
/// <para>The primitive element <see cref="IReference"/> is supported.</para>
/// </summary>
public interface IObservationPerformer : IReference;
