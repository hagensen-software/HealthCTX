using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Organizations.Interfaces;

[FhirResource("Organization")]
public interface IOrganization : IResource;
