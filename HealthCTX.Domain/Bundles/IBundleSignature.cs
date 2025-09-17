using HealthCTX.Domain.Signatures;

namespace HealthCTX.Domain.Bundles;

/// <summary>
/// <para>Interface for HL7 FHIR Bundle signature.</para>
/// <para>The elements from <see cref="ISignature"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IBundleSignature : ISignature;
