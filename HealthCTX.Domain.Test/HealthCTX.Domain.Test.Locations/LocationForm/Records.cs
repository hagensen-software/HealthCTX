using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Locations;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Locations.LocationForm;

public record FormSystem(Uri Value) : ICodingSystem;
public record FormCode(string Value) : ICodingCode;
public record FormCoding(FormSystem? System, FormCode? Code) : ICodeableConceptCoding;
public record LocationForm(ImmutableList<FormCoding> Codings) : ILocationForm;
public record Location(LocationForm? Form) : ILocation;

