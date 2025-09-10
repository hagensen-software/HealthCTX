using HealthCTX.Domain.Annotation;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation note.</para>
/// <para>The elements from <see cref="IAnnotation"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationNote : IAnnotation;
