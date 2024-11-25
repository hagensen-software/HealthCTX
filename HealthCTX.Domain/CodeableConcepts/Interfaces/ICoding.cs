using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirElement]
[FhirProperty("system", typeof(ISystemUri))]
[FhirProperty("version", typeof(IVersionString))]
[FhirProperty("code", typeof(ICodingCode))]
[FhirProperty("display", typeof(IDisplayString))]
[FhirProperty("userSelected", typeof(IUserSelectedBoolean))]
public interface ICoding : IElement;
