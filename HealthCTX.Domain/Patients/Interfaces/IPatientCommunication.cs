using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("language", typeof(IPatientCommunicationLanguage), Cardinality.Mandatory)]
[FhirProperty("preferred", typeof(IPatientCommunicationPreferred), Cardinality.Optional)]
public interface IPatientCommunication : IElement
{
}
