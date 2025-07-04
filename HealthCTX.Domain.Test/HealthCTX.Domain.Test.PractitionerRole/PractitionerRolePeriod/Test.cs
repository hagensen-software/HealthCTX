using System.Text.Json;

namespace HealthCTX.Domain.Test.PractitionerRole.PractitionerRolePeriod;

public class Test
{
    private readonly DateTimeOffset periodStartDate = new(2023, 10, 1, 0, 0, 0, TimeSpan.Zero);
    private readonly DateTimeOffset periodEndDate = new(2023, 10, 31, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public void PractitionerRole_ToFhirJsonGeneratesJsonString()
    {
        var practitionerRole = new PractitionerRole(
            new PractitionerRolePeriod(
                new PeriodStart(periodStartDate),
                new PeriodEnd(periodEndDate)));

        (var jsonString, _) = PractitionerRoleFhirJsonMapper.ToFhirJsonString(practitionerRole);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;
        var period = root.GetProperty("period");

        Assert.Equal(periodStartDate, period.GetProperty("start").GetDateTimeOffset());
        Assert.Equal(periodEndDate, period.GetProperty("end").GetDateTimeOffset());
    }

    [Fact]
    public void PractitionerRole_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "PractitionerRole",
                "period" : {
                    "start" : "2023-10-01T00:00+00:00",
                    "end" : "2023-10-31T00:00+00:00"
                }
            }
            """;

        (var practitionerRole, var outcomes) = PractitionerRoleFhirJsonMapper.ToPractitionerRole(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(periodStartDate, practitionerRole?.Period.Start?.Value);
        Assert.Equal(periodEndDate, practitionerRole?.Period.End?.Value);
    }
}
