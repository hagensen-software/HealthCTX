using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirElement]
[FhirProperty("system", typeof(ICodingSystem), Cardinality.Single)]
[FhirProperty("version", typeof(ICodingVersion), Cardinality.Single)]
[FhirProperty("code", typeof(ICodingCode), Cardinality.Single)]
[FhirProperty("display", typeof(ICodingDisplay), Cardinality.Single)]
[FhirProperty("userSelected", typeof(ICodingUserSelected), Cardinality.Single)]
public interface ICodeableConceptCoding : IElement;
