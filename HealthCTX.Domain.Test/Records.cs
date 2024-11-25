using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test;

public record MaritalStatusSystem(Uri Value) : ISystemUri;
public record MaritalStatusVersion(string Value) : IVersionString;
public record MaritalStatusCode(string Value) : ICodingCode;
public record MaritalStatusDisplay(string Value) : IDisplayString;
public record MaritalStatusUserSelected(bool Value) : IUserSelectedBoolean;
public record MaritalStatusCoding(
    MaritalStatusSystem System,
    MaritalStatusVersion Version,
    MaritalStatusCode Code,
    MaritalStatusDisplay Display,
    MaritalStatusUserSelected? UserSelected) : ICoding;
public record MaritalStatusText(string Value) : ICodeableConceptText;

public record MaritalStatus(ImmutableList<MaritalStatusCoding> Coding, MaritalStatusText Text) : IMaritalStatusCodeableConcept;

public record Patient(MaritalStatus? MaritalStatus) : IPatient;
