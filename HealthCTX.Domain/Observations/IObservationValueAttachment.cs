using HealthCTX.Domain.Attachments;

namespace HealthCTX.Domain.Observations;

/// <summary>
/// <para>Interface for HL7 FHIR Observation value[Attachment].</para>
/// <para>The elements from <see cref="IAttachment"/> are supported and may be added as (a collection of) a property implementing the corresponding interfaces.</para>
/// </summary>
public interface IObservationValueAttachment : IAttachment;
