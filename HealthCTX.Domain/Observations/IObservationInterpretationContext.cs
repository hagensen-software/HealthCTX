using HealthCTX.Domain.CodeableReferences;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation interpretationContext (R6).</para>
/// <para>The elements from <see cref="ICodeableReference"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationInterpretationContext : ICodeableReference;
