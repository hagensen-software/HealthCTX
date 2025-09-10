using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Locations;

/// <summary>
/// <para>Interface for HL7 FHIR Location form.</para>
/// <para>The elements from <see cref="ICodeableConcept"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ILocationForm : ICodeableConcept;