using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.HumanName.Interfaces;

[FhirElement]
[FhirProperty("use", typeof(IHumanNameUse), Cardinality.Single)]
[FhirProperty("text", typeof(IHumanNameText), Cardinality.Single)]
[FhirProperty("family", typeof(IHumanNameFamily), Cardinality.Single)]
[FhirProperty("given", typeof(IHumanNameGiven), Cardinality.Multiple)]
[FhirProperty("prefix", typeof(IHumanNamePrefix), Cardinality.Multiple)]
[FhirProperty("suffix", typeof(IHumanNameSuffix), Cardinality.Multiple)]
[FhirProperty("period", typeof(IHumanNamePeriod), Cardinality.Single)]
public interface IHumanName : IElement;
