using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for the HL7 FHIR ELEMENT ATTRIBUTE element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>longitude</term>
///         <description><see cref="ILocationPositionLongitude"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>latitude</term>
///         <description><see cref="ILocationPositionLatitude"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>altitude</term>
///         <description><see cref="ILocationPositionAltitude"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("longitude", typeof(ILocationPositionLongitude), Cardinality.Mandatory)]
[FhirProperty("latitude", typeof(ILocationPositionLatitude), Cardinality.Mandatory)]
[FhirProperty("altitude", typeof(ILocationPositionAltitude), Cardinality.Optional)]
public interface ILocationPosition : IElement;
