using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.CodeableConcepts;

[FhirElement]
[FhirProperty("system", typeof(ICodingSystem), Cardinality.Optional)]
[FhirProperty("version", typeof(ICodingVersion), Cardinality.Optional)]
[FhirProperty("code", typeof(ICodingCode), Cardinality.Optional)]
[FhirProperty("display", typeof(ICodingDisplay), Cardinality.Optional)]
[FhirProperty("userSelected", typeof(ICodingUserSelected), Cardinality.Optional)]
public interface ICodeableConceptCoding : IElement;
