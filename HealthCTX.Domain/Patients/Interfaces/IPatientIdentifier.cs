using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;
using HealthCTX.Domain.Identifiers.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirElement]
[FhirProperty("use", typeof(IIdentifierUse), Cardinality.Single)]
[FhirProperty("type", typeof(IIdentifierType), Cardinality.Single)]
[FhirProperty("system", typeof(IIdentifierSystem), Cardinality.Single)]
[FhirProperty("value", typeof(IIdentifierValue), Cardinality.Single)]
[FhirProperty("period", typeof(IIdentifierPeriod), Cardinality.Single)]
public interface IPatientIdentifier : IElement;
