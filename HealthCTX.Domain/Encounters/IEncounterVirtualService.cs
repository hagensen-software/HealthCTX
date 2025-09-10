using HealthCTX.Domain.VirtualServiceDetail;

namespace HealthCTX.Domain.Encounters;

/// <summary>
/// <para>Interface for HL7 FHIR Encounter virtualService.</para>
/// <para>The elements from <see cref="IVirtualServiceDetail"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IEncounterVirtualService : IVirtualServiceDetail;
