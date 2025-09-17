using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for the HL7 FHIR Encounter location element.</para>
/// <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
/// <list type="table">
///     <item>
///         <term>location</term>
///         <description><see cref="IEncounterLocationLocation"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>status</term>
///         <description><see cref="IEncounterLocationStatus"/> (HL7 FHIR R4/R5)</description>
///     </item>
///     <item>
///         <term>physicalType</term>
///         <description><see cref="IEncounterLocationPhysicalType"/> (HL7 FHIR R4 Only)</description>
///     </item>
///     <item>
///         <term>form</term>
///         <description><see cref="IEncounterLocationForm"/> (HL7 FHIR R5 Only)</description>
///     </item>
///     <item>
///         <term>period</term>
///         <description><see cref="IEncounterLocationPeriod"/> (HL7 FHIR R4/R5)</description>
///     </item>
/// </list>
/// </summary>
[FhirElement]
[FhirProperty("location", typeof(IEncounterLocationLocation), Cardinality.Mandatory)]
[FhirProperty("status", typeof(IEncounterLocationStatus), Cardinality.Optional)]
[FhirProperty("physicalType", typeof(IEncounterLocationPhysicalType), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("form", typeof(IEncounterLocationForm), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("period", typeof(IEncounterLocationPeriod), Cardinality.Optional)]
public interface IEncounterLocation : IElement;
