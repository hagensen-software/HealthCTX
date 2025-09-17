using HealthCTX.Domain.Quantities;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation referenceRange.high and component.referenceRange.high.</para>
/// <para>The elements from <see cref="ISimpleQuantity"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationReferenceRangeHigh : ISimpleQuantity;
