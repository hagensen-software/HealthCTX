using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Extensions.PatientHumanNames;

[FhirFixedValue("use", "official")]
public interface IOfficialName : IPatientHumanName;

[FhirFixedValue("use", "nickname")]
public interface INickname : IPatientHumanName;

public record FamilyName(string Value) : IHumanNameFamily;
public record GivenName(string Value) : IHumanNameGiven;
public record OfficialName(FamilyName FamilyName, GivenName GivenName) : IOfficialName;

public record NameText(string Value) : IHumanNameText;
public record Nickname(NameText Text) : INickname;

[FhirValueSlicing("name", "use", typeof(IOfficialName), Cardinality.Mandatory)]
[FhirValueSlicing("name", "use", typeof(INickname), Cardinality.Optional)]
public interface IMyPatient : IPatient;
public record Patient(OfficialName Name, Nickname? Nickname) : IMyPatient;
