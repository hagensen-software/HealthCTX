using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.VirtualServiceDetail;

namespace HealthCTX.Domain.Locations;

/// <summary>
///     <para>Interface for the HL7 FHIR Location resource.</para>
///     <para>The following elements are supported and may be added as (a collection of) a property implementing the corresponding interface as listed below.</para>
///     <list type="table">
///         <item>
///             <term>identifier</term>
///             <description><see cref="ILocationIdentifier"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>status</term>
///             <description><see cref="ILocationStatus"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>operationalStatus</term>
///             <description><see cref="ILocationOperationalStatus"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>name</term>
///             <description><see cref="ILocationName"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>alias</term>
///             <description><see cref="ILocationAlias"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>description</term>
///             <description><see cref="ILocationDescription"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>mode</term>
///             <description><see cref="ILocationMode"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>type</term>
///             <description><see cref="ILocationType"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>telecom</term>
///             <description><see cref="ILocationTelecom"/> (HL7 FHIR R4)</description>
///         </item>
///         <item>
///             <term>contact</term>
///             <description><see cref="ILocationContact"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>address</term>
///             <description><see cref="ILocationAddress"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>physicalType</term>
///             <description><see cref="ILocationPhysicalType"/> (HL7 FHIR R4)</description>
///         </item>
///         <item>
///             <term>form</term>
///             <description><see cref="ILocationForm"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>position</term>
///             <description><see cref="ILocationPosition"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>managingOrganization</term>
///             <description><see cref="ILocationManagingOrganization"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>partOf</term>
///             <description><see cref="ILocationPartOf"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>characteristic</term>
///             <description><see cref="ILocationCharacteristic"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>hoursOfOperation</term>
///             <description><see cref="ILocationHoursOfOperation"/> (HL7 FHIR R4/R5)</description>
///         </item>
///         <item>
///             <term>availabilityExceptions</term>
///             <description><see cref="ILocationAvailabilityExceptions"/> (HL7 FHIR R4)</description>
///         </item>
///         <item>
///             <term>virtualService</term>
///             <description><see cref="IVirtualServiceDetail"/> (HL7 FHIR R5)</description>
///         </item>
///         <item>
///             <term>endpoint</term>
///             <description><see cref="ILocationEndpoint"/> (HL7 FHIR R4/R5)</description>
///         </item>
///     </list>
/// </summary>
[FhirResource("Location")]
[FhirProperty("identifier", typeof(ILocationIdentifier), Cardinality.Multiple)]
[FhirProperty("status", typeof(ILocationStatus), Cardinality.Optional)]
[FhirProperty("operationalStatus", typeof(ILocationOperationalStatus), Cardinality.Optional)]
[FhirProperty("name", typeof(ILocationName), Cardinality.Optional)]
[FhirProperty("alias", typeof(ILocationAlias), Cardinality.Multiple)]
[FhirProperty("description", typeof(ILocationDescription), Cardinality.Optional)]
[FhirProperty("mode", typeof(ILocationMode), Cardinality.Optional)]
[FhirProperty("type", typeof(ILocationType), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(ILocationTelecom), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("contact", typeof(ILocationContact), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("address", typeof(ILocationAddress), Cardinality.Optional)]
[FhirProperty("physicalType", typeof(ILocationPhysicalType), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("form", typeof(ILocationForm), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("position", typeof(ILocationPosition), Cardinality.Optional)]
[FhirProperty("managingOrganization", typeof(ILocationManagingOrganization), Cardinality.Optional)]
[FhirProperty("partOf", typeof(ILocationPartOf), Cardinality.Optional)]
[FhirProperty("characteristic", typeof(ILocationCharacteristic), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("hoursOfOperation", typeof(ILocationHoursOfOperation), Cardinality.Multiple)]
[FhirProperty("availabilityExceptions", typeof(ILocationAvailabilityExceptions), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("virtualService", typeof(IVirtualServiceDetail), Cardinality.Multiple, FromVersion: FhirVersion.R5)]
[FhirProperty("endpoint", typeof(ILocationEndpoint), Cardinality.Multiple)]
public interface ILocation : IResource;
