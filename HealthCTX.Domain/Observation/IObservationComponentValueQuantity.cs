using HealthCTX.Domain.Quantity;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation component.value[Quantity].</para>
/// <para>The primitive element <see cref="IQuantity"/> is supported.</para>
/// </summary>
public interface IObservationComponentValueQuantity : IQuantity;
