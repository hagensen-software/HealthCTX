using HealthCTX.Domain.CodeableConcepts;
using HealthCTX.Domain.Identifiers;

namespace HealthCTX.Domain.Signatures;

/// <summary>
/// <para>Interface for HL7 FHIR Signature type.</para>
/// <para>The elements from <see cref="ICodeableConceptCoding"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ISignatureType : ICodeableConceptCoding;
