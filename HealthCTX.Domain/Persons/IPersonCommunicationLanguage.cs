using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Persons;

/// <summary>
/// <para>Interface for HL7 FHIR Person communication.language.</para>
/// <para>The elements from <see cref="ICodeableConcept"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IPersonCommunicationLanguage : ICodeableConcept;