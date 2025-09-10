using HealthCTX.Domain.Quantity;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation referenceRange.low and component.referenceRange.low.</para>
/// <para>The primitive element <see cref="ISimpleQuantity"/> is supported.</para>
/// </summary>
public interface IObservationReferenceRangeLow : ISimpleQuantity;
