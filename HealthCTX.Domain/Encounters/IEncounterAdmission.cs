using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.Encounters;

[FhirElement]
[FhirProperty("preAdmissionIdentifier", typeof(IEncounterAdmissionPreAdmissionIdentifier), Cardinality.Optional)]
[FhirProperty("origin", typeof(IEncounterAdmissionOrigin), Cardinality.Optional)]
[FhirProperty("admitSource", typeof(IEncounterAdmissionAdmitSource), Cardinality.Optional)]
[FhirProperty("reAdmission", typeof(IEncounterAdmissionReAdmission), Cardinality.Optional)]
[FhirProperty("destination", typeof(IEncounterAdmissionDestination), Cardinality.Optional)]
[FhirProperty("dischargeDisposition", typeof(IEncounterAdmissionDischargeDisposition), Cardinality.Optional)]
public interface IEncounterAdmission : IElement;
