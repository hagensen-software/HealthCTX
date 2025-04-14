using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Practitioners;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Practitioner.PractionerCommunication;

public record PractitionerCommunicationLanguageCode(string Value) : ICodingCode;
public record PractitionerCommunicationLanguageCoding(PractitionerCommunicationLanguageCode Code) : ICodeableConceptCoding;
public record PractitionerCommunicationLanguage(PractitionerCommunicationLanguageCoding Coding) : IPractitionerCommunicationLanguage;
public record PractitionerCommunicationPreferred(bool Value) : IPractitionerLanguagePreferred;

public record PractitionerCommunication(PractitionerCommunicationLanguage Languages, PractitionerCommunicationPreferred? Preferred) : IPractitionerCommunication;

public record Practitioner(ImmutableList<PractitionerCommunication> Communication) : IPractitioner;


public record PractitionerCommunicationR4(PractitionerCommunicationLanguageCoding Coding) : IPractitionerCommunication;

public record PractitionerR4(ImmutableList<PractitionerCommunicationR4> Communication) : IPractitioner;