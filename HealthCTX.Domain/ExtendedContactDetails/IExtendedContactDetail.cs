﻿using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.ExtendedContactDetails;

[FhirElement]
[FhirProperty("purpose", typeof(IExtendedContactDetailPurpose), Cardinality.Optional)]
[FhirProperty("name", typeof(IExtendedContactDetailName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IExtendedContactDetailTelecom), Cardinality.Multiple)]
[FhirProperty("address", typeof(IExtendedContactDetailAddress), Cardinality.Optional)]
[FhirProperty("organization", typeof(IExtendedContactDetailOrganization), Cardinality.Optional, Framework.FhirVersion.R5)]
[FhirProperty("period", typeof(IExtendedContactDetailPeriod), Cardinality.Optional, Framework.FhirVersion.R5)]
public interface IExtendedContactDetail : IElement;
