using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirResource("Patient")]
[FhirProperty("identifier", typeof(IPatientIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPatientActive), Cardinality.Optional)]
[FhirProperty("name", typeof(IPatientHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPatientContactPoint), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPatientGender), Cardinality.Optional)]
[FhirProperty("birthDate", typeof(IPatientBirthDate), Cardinality.Optional)]
[FhirProperty("deceased[Boolean]", typeof(IPatientDeceasedBoolean), Cardinality.Optional)]
[FhirProperty("deceased[DateTime]", typeof(IPatientDeceasedDateTime), Cardinality.Optional)]
[FhirProperty("address", typeof(IPatientAddress), Cardinality.Multiple)]
[FhirProperty("maritalStatus", typeof(IPatientMaritalStatus), Cardinality.Optional)]
[FhirProperty("multipleBirth[Boolean]", typeof(IPatientMultipleBirthBoolean), Cardinality.Optional)]
[FhirProperty("multipleBirth[Integer]", typeof(IPatientMultipleBirthInteger), Cardinality.Optional)]
[FhirProperty("photo", typeof(IPatientPhoto), Cardinality.Multiple)]
[FhirProperty("contact", typeof(IPatientContact), Cardinality.Multiple)]
[FhirProperty("communication", typeof(IPatientCommunication), Cardinality.Multiple)]
[FhirProperty("generalPractitioner", typeof(IPatientGeneralPractitioner), Cardinality.Multiple)]
[FhirProperty("managingOrganization", typeof(IPatientManagingOrganization), Cardinality.Optional)]
[FhirProperty("link", typeof(IPatientLink), Cardinality.Multiple)]
public interface IPatient : IResource;
