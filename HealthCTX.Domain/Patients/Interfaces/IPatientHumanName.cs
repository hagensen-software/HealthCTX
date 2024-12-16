using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.HumanName.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("use", typeof(IHumanNameUse), Cardinality.Single)]
[FhirProperty("text", typeof(IHumanNameText), Cardinality.Single)]
[FhirProperty("family", typeof(IHumanNameFamily), Cardinality.Single)]
[FhirProperty("given", typeof(IHumanNameFamily), Cardinality.Multiple)]
[FhirProperty("prefix", typeof(IHumanNamePrefix), Cardinality.Multiple)]
[FhirProperty("suffix", typeof(IHumanNameSuffix), Cardinality.Multiple)]
[FhirProperty("period", typeof(IHumanNamePeriod), Cardinality.Single)]
public interface IPatientHumanName
{
}
