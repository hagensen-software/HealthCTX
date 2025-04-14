using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Patients;

[FhirElement]
[FhirProperty("language", typeof(IPatientCommunicationLanguage), Cardinality.Mandatory)]
[FhirProperty("preferred", typeof(IPatientCommunicationPreferred), Cardinality.Optional)]
public interface IPatientCommunication : IElement;
