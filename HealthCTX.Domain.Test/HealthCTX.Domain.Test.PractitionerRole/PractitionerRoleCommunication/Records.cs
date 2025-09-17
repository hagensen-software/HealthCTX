using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.PractitionerRoles;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCommunication;

public record CommunicationCode(string Value) : ICodingCode;
public record CommunicationSystem(Uri Value) : ICodingSystem;
public record PractitionerRoleCommunicationCoding(
    CommunicationCode Code,
    CommunicationSystem System) : ICodeableConceptCoding;

public record PractitionerRoleCommunication(PractitionerRoleCommunicationCoding Coding) : IPractitionerRoleCommunication;

public record PractitionerRole(ImmutableList<PractitionerRoleCommunication> Communications) : IPractitionerRole;
