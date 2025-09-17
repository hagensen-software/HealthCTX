using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Availabilities;

/// <summary>
/// <para>Interface for the HL7 FHIR Availability notAvailableTime.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>description</term>
///         <description><see cref="IAvailabilityNotAvailableDescription"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>during</term>
///         <description><see cref="IAvailabilityNotAvailableDuring"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("description", typeof(IAvailabilityNotAvailableDescription), Cardinality.Optional)]
[FhirProperty("during", typeof(IAvailabilityNotAvailableDuring), Cardinality.Optional)]
public interface IAvailabilityNotAvailable : IElement;
