using HealthCTX.Domain.HumanNames;
using HealthCTX.Domain.HumanNames.Model;
using System.Collections.Immutable;

namespace HealthCTX.Domain.Test.Model.HumanNames;

public record HumanName(
    HumanNameUse? Use,
    ImmutableList<HumanNameGiven> Givens,
    HumanNameFamily? Family,
    HumanNamePrefix? Prefix,
    HumanNameSuffix? Suffix,
    HumanNameText? Text,
    HumanNamePeriod? Period
    ) : IHumanName;
