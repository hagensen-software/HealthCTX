using HealthCTX.Domain.Attributes;
using HealthCTX.Domain.Extensions;
using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Patients;

namespace HealthCTX.Domain.Test.Slicing.Extensions;

[FhirFixedValue("url", "http://example.org/fhir/StructureDefinition/humanname-middle")]
public interface IHumanNameMiddle : IExtension;

[FhirValueSlicing("extension", "url", typeof(IHumanNameMiddle), Cardinality.Optional)]
public interface IOfficialName : IPatientHumanName;

public interface IMyPatient : IPatient;


public record FamilyName(string Value) : IHumanNameFamily;
public record MiddleNameText(string Value) : IStringPrimitive;
public record MiddleName(MiddleNameText Text) : IHumanNameMiddle;
public record GivenName(string Value) : IHumanNameGiven;

public record OfficialName(FamilyName FamilyName, MiddleName MiddleName, GivenName GivenName) : IOfficialName;

public record Patient(OfficialName Name) : IMyPatient;
