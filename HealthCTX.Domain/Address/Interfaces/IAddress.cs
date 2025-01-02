using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Address.Interfaces;

[FhirElement]
[FhirProperty("use", typeof(IAddressUse), Cardinality.Single)]
[FhirProperty("type", typeof(IAddressType), Cardinality.Single)]
[FhirProperty("text", typeof(IAddressText), Cardinality.Single)]
[FhirProperty("line", typeof(IAddressLine), Cardinality.Multiple)]
[FhirProperty("city", typeof(IAddressCity), Cardinality.Single)]
[FhirProperty("district", typeof(IAddressDistrict), Cardinality.Single)]
[FhirProperty("state", typeof(IAddressState), Cardinality.Single)]
[FhirProperty("postalCode", typeof(IAddressPostalCode), Cardinality.Single)]
[FhirProperty("country", typeof(IAddressCountry), Cardinality.Single)]
[FhirProperty("period", typeof(IAddressPeriod), Cardinality.Single)]
public interface IAddress : IElement;
