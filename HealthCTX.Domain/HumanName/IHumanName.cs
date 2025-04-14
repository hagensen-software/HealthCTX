using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.HumanName;

[FhirElement]
[FhirProperty("use", typeof(IHumanNameUse), Cardinality.Optional)]
[FhirProperty("text", typeof(IHumanNameText), Cardinality.Optional)]
[FhirProperty("family", typeof(IHumanNameFamily), Cardinality.Optional)]
[FhirProperty("given", typeof(IHumanNameGiven), Cardinality.Multiple)]
[FhirProperty("prefix", typeof(IHumanNamePrefix), Cardinality.Multiple)]
[FhirProperty("suffix", typeof(IHumanNameSuffix), Cardinality.Multiple)]
[FhirProperty("period", typeof(IHumanNamePeriod), Cardinality.Optional)]
public interface IHumanName : IElement;
