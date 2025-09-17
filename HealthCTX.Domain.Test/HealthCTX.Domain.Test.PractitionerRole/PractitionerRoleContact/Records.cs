using HealthCTX.Domain.Addresses;
using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.ContactPoints;
using HealthCTX.Domain.ExtendedContactDetails;
using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.Periods;
using HealthCTX.Domain.PractitionerRoles;
using HealthCTX.Domain.References;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRoleContact;

public record ContactDetailPurposeSystem(Uri Value) : ICodingSystem;
public record ContactDetailPurposeCode(string Value) : ICodingCode;
public record ContactDetailPurposeCoding(ContactDetailPurposeSystem System, ContactDetailPurposeCode Code) : ICodeableConceptCoding;
public record ContactDetailPurpose(ContactDetailPurposeCoding Coding) : IExtendedContactDetailPurpose;

public record ContactDetailFamilyName(string Value) : IHumanNameFamily;
public record ContactDetailName(ContactDetailFamilyName FamilyName) : IExtendedContactDetailName;

public record ContactDetailTeleconSystem(string Value) : IContactPointSystem;
public record ContactDetailTeleconValue(string Value) : IContactPointValue;
public record ContactDetailTelecom(ContactDetailTeleconSystem System, ContactDetailTeleconValue Value) : IExtendedContactDetailTelecom;

public record ContactDetailAddressText(string Value) : IAddressText;
public record ContactDetailAddress(ContactDetailAddressText Text) : IExtendedContactDetailAddress;

public record ContactDetailOrganizationReference(string Value) : IReferenceReference;
public record ContactDetailOrganization(ContactDetailOrganizationReference Reference) : IExtendedContactDetailOrganization;

public record ContactDetailPeriodStart(DateTimeOffset Value) : IPeriodStart;
public record ContactDetailPeriod(ContactDetailPeriodStart Start) : IExtendedContactDetailPeriod;

public record PractitionerRoleContact
    (
        ContactDetailPurpose Purpose,
        ContactDetailName Name,
        ContactDetailTelecom Telecom,
        ContactDetailAddress Address,
        ContactDetailOrganization? Organization,
        ContactDetailPeriod? Period
    ) : IPractitionerRoleContact;

public record PractitionerRole(PractitionerRoleContact Contact) : IPractitionerRole;
