using HealthCTX.Domain.Annotation;

namespace HealthCTX.Domain.Observation;

/// <summary>
/// <para>Interface for HL7 FHIR Observation note.</para>
/// <para>The primitive element <see cref="IAnnotation"/> is supported.</para>
/// </summary>
public interface IObservationNote : IAnnotation;
