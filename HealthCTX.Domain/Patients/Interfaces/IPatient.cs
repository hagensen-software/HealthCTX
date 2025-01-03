﻿using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Patients.Interfaces;

[FhirResource("Patient")]
[FhirProperty("identifier", typeof(IPatientIdentifier), Cardinality.Multiple)]
[FhirProperty("active", typeof(IPatientActive), Cardinality.Single)]
[FhirProperty("name", typeof(IPatientHumanName), Cardinality.Multiple)]
[FhirProperty("telecom", typeof(IPatientContactPoint), Cardinality.Multiple)]
[FhirProperty("gender", typeof(IPatientGender), Cardinality.Single)]
[FhirProperty("birthDate", typeof(IPatientBirthDate), Cardinality.Single)]
[FhirProperty("deceased[Boolean]", typeof(IPatientDeceasedBoolean), Cardinality.Single)]
[FhirProperty("deceased[DateTime]", typeof(IPatientDeceasedDateTime), Cardinality.Single)]
[FhirProperty("address", typeof(IPatientAddress), Cardinality.Multiple)]
[FhirProperty("maritalStatus", typeof(IPatientMaritalStatus), Cardinality.Single)]
[FhirProperty("multipleBirth[Boolean]", typeof(IPatientMultipleBirthBoolean), Cardinality.Single)]
[FhirProperty("multipleBirth[Integer]", typeof(IPatientMultipleBirthInteger), Cardinality.Single)]
[FhirProperty("photo", typeof(IPatientPhoto), Cardinality.Multiple)]
[FhirProperty("contact", typeof(IPatientContact), Cardinality.Multiple)]
public interface IPatient : IResource;
