using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.PractitionerRole;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleCharacteristic;

public record CharacteristicCode(string Value) : ICodingCode;
public record CharacteristicSystem(Uri Value) : ICodingSystem;
public record PractitionerRoleCharacteristicCoding(
    CharacteristicCode Code,
    CharacteristicSystem System) : ICodeableConceptCoding;

public record PractitionerRoleCharacteristic(PractitionerRoleCharacteristicCoding Coding) : IPractitionerRoleCharacteristic;

public record PractitionerRole(ImmutableList<PractitionerRoleCharacteristic> Characteristics) : IPractitionerRole;
