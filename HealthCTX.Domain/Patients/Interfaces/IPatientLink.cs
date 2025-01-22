using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("other", typeof(IPatientLinkOther), Cardinality.Mandatory)]
[FhirProperty("type", typeof(IPatientLinkType), Cardinality.Mandatory)]
public interface IPatientLink : IElement;
