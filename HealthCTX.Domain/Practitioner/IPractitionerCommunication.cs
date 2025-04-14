using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.CodeableConcepts;

namespace HealthCTX.Domain.Practitioner;

[FhirElement]
[FhirProperty("coding", typeof(ICodeableConceptCoding), Cardinality.Multiple, ToVersion: FhirVersion.R4)]
[FhirProperty("text", typeof(ICodeableConceptText), Cardinality.Optional, ToVersion: FhirVersion.R4)]
[FhirProperty("language", typeof(IPractitionerCommunicationLanguage), Cardinality.Mandatory, FromVersion: FhirVersion.R5)]
[FhirProperty("preferred", typeof(IPractitionerLanguagePreferred), Cardinality.Optional, FromVersion: FhirVersion.R5)]
public interface IPractitionerCommunication : IElement;
