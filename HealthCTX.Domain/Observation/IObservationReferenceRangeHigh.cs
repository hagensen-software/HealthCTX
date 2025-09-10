using HealthCTX.Domain.Quantity;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation referenceRange.high and component.referenceRange.high.</para>
/// <para>The primitive element <see cref="ISimpleQuantity"/> is supported.</para>
/// </summary>
public interface IObservationReferenceRangeHigh : ISimpleQuantity;
