using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Practitioners;

[FhirResource("Practitioner")]
[FhirProperty("identifier",typeof(IPractitionerIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPractitionerActive), Cardinality.Optional)]
[FhirProperty("name", typeof(IPractitionerHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPractitionerTelecom), Cardinality.Multiple)]
[FhirProperty("address", typeof(IPractitionerAddress), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPractitionerGender), Cardinality.Optional)]
[FhirProperty("birthDate", typeof(IPractitionerBirthDate), Cardinality.Optional)]
[FhirProperty("photo", typeof(IPractitionerPhoto), Cardinality.Multiple)]
[FhirProperty("qualification", typeof(IPractitionerQualification), Cardinality.Multiple)]
[FhirProperty("communication", typeof(IPractitionerCommunication), Cardinality.Multiple)]
public interface IPractitioner : IResource;
