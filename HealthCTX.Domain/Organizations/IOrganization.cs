using HealthCTX.Domain.Framework.Attributes;
using HealthCTX.Domain.Framework.Interfaces;

namespace HealthCTX.Domain.Organizations;

[FhirResource("Organization")]
[FhirProperty("contact", typeof(IOrganizationContact), Cardinality.Multiple)]
public interface IOrganization : IResource;
