using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation interpretation.</para>
/// <para>The elements from <see cref="ICodeableConcept"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationInterpretation : ICodeableConcept;
