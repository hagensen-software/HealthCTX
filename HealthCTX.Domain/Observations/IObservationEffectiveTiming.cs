using HealthCTX.Domain.Timings;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation effektive[Timing].</para>
/// <para>The elements from <see cref="ITiming"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationEffectiveTiming : ITiming;
