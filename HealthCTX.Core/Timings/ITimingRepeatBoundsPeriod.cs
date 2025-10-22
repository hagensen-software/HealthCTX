using HealthCTX.Domain.Periods;

namespace HealthCTX.Domain.Timings;

/// <summary>
/// <para>Interface for HL7 FHIR Timing bounds[Period].</para>
/// <para>The elements from <see cref="IPeriod"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ITimingRepeatBoundsPeriod : IPeriod;
