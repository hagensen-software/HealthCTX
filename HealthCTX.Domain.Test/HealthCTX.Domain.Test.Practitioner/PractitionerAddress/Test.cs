﻿using System.Text.Json;

namespace HealthCTX.Domain.Test.Practitioner.PractitionerAddress;

public class Test
{
    private readonly DateTimeOffset periodStartDate = new(2024, 2, 14, 13, 42, 0, new TimeSpan(1, 0, 0));
    private readonly string periodStartDateString = "2024-02-14T13:42:00+01:00";
    private readonly DateTimeOffset periodEndDate = new(2024, 1, 31, 0, 0, 0, new TimeSpan(1, 0, 0));
    private readonly string periodEndDateString = "2024-01-31T00:00:00+01:00";

    [Fact]
    public void Practitioner_ToFhirJsonGeneratesJsonString()
    {
        var practitioner = new Practitioner(
            [new PractitionerAddress(
                new AddressUse("home"),
                new AddressType("both"),
                new AddressText("some address"),
                [new AddressLine("some line"), new AddressLine("another line")],
                new AddressCity("some city"),
                new AddressDistrict("some district"),
                new AddressState("some state"),
                new AddressPostalCode("some postal code"),
                new AddressCountry("some country"),
                new AddressPeriod(
                    new PeriodStart(periodStartDate),
                    new PeriodEnd(periodEndDate))
                )]);

        (var jsonString, _) = PractitionerFhirJsonMapper.ToFhirJsonString(practitioner);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;
        var addresses = root.GetProperty("address");
        var address = addresses.EnumerateArray().GetEnumerator().First();
        var use = address.GetProperty("use").GetString();
        var lines = address.GetProperty("line");
        var period = address.GetProperty("period");

        Assert.Equal(JsonValueKind.Object, address.ValueKind);
        Assert.Equal("home", use);
        Assert.Equal(JsonValueKind.Array, lines.ValueKind);
        Assert.Equal(JsonValueKind.Object, period.ValueKind);
    }

    [Fact]
    public void Practitioner_FromFhirJsonGeneratesRecords()
    {
        var jsonString = $$"""
            {
                "resourceType" : "Practitioner",
                "address" : [
                    {
                        "use" : "home",
                        "type" : "both",
                        "text" : "some address",
                        "line" : [ "some line", "another line" ],
                        "city" : "some city",
                        "district" : "some district",
                        "state" : "some state",
                        "postalCode" : "some postal code",
                        "country" : "some country",
                        "period" : {
                            "start" : "{{periodStartDateString}}",
                            "end" : "{{periodEndDateString}}"
                        }
                    }
                ]
            }
            """;

        (var practitioner, var outcomes) = PractitionerFhirJsonMapper.ToPractitioner(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal("home", practitioner?.Address.First().Use.Value);
        Assert.Collection(practitioner?.Address.First().Lines!,
            l => Assert.Equal("some line", l.Value),
            l => Assert.Equal("another line", l.Value));
        Assert.Equal(periodStartDate, practitioner?.Address.First().Period.Start.Value);
        Assert.Equal(periodEndDate, practitioner?.Address.First().Period.End.Value);
    }
}
