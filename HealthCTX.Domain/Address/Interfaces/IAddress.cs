using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Address.Interfaces;

[FhirElement]
[FhirProperty("use", typeof(IAddressUse), Cardinality.Optional)]
[FhirProperty("type", typeof(IAddressType), Cardinality.Optional)]
[FhirProperty("text", typeof(IAddressText), Cardinality.Optional)]
[FhirProperty("line", typeof(IAddressLine), Cardinality.Multiple)]
[FhirProperty("city", typeof(IAddressCity), Cardinality.Optional)]
[FhirProperty("district", typeof(IAddressDistrict), Cardinality.Optional)]
[FhirProperty("state", typeof(IAddressState), Cardinality.Optional)]
[FhirProperty("postalCode", typeof(IAddressPostalCode), Cardinality.Optional)]
[FhirProperty("country", typeof(IAddressCountry), Cardinality.Optional)]
[FhirProperty("period", typeof(IAddressPeriod), Cardinality.Optional)]
public interface IAddress : IElement;
