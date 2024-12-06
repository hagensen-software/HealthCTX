﻿using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.CodeableConcepts.Interfaces;

[FhirPrimitive]
public interface ISystemUri : IElement
{
    [FhirIgnore]
    Uri Value { get; init; }
}
