using HealthCTX.Domain.Quantities;

namespace HealthCTX.Domain.Ratios;

/// <summary>
/// <para>Interface for HL7 FHIR Ratio numerator.</para>
/// <para>The elements from <see cref="IQuantity"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IRatioNumerator : IQuantity;
