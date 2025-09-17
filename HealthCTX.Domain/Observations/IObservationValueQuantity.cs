using HealthCTX.Domain.Quantities;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation value[Quantity].</para>
/// <para>The elements from <see cref="IQuantity"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationValueQuantity : IQuantity;
