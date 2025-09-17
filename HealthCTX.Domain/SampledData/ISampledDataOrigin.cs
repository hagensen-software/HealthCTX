using HealthCTX.Domain.Quantity;

namespace HealthCTX.Domain.SampledData;

/// <summary>
/// <para>Interface for HL7 FHIR SampledData origin.</para>
/// <para>The elements from <see cref="ISimpleQuantity"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface ISampledDataOrigin : ISimpleQuantity;
