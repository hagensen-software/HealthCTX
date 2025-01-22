using HealthCTX.Domain.CodeableConcepts.Interfaces;
using HealthCTX.Domain.Patients.Interfaces;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Patients.PatientCommunication;

public record PatientLanguageCode(string Value) : ICodingCode;
public record PatientLanguageSystem(Uri Value) : ICodingSystem;
public record PatientLanguageDisplay(string Value) : ICodingDisplay;
public record PatientLanguageCoding(PatientLanguageCode Code, PatientLanguageSystem System, PatientLanguageDisplay Display) : ICodeableConceptCoding;
public record PatientCommunicationLanguage(PatientLanguageCoding Coding) : IPatientCommunicationLanguage;

public record PatientPreferred(bool Value) : IPatientCommunicationPreferred;

public record PatientCommunication(PatientCommunicationLanguage Language, PatientPreferred Preferred) : IPatientCommunication;

public record Patient(ImmutableList<PatientCommunication> Communication) : IPatient;
