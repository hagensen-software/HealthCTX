using HealthCTX.Domain.Attributes;
using System.Text.Json;

namespace HealthCTX.Domain.Test.Observation.ObservationValueSampledData;

public class Test
{
    [Fact]
    public void Observation_ToR4FhirJsonGeneratesJsonString()
    {
        var observation = new ObservationR4(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))),
            new SampledDataR4(
                new OriginQuantity(new OriginValue(2.0)),
                new Period(3.0),
                new Factor(4.0),
                new LowerLimit(5.0),
                new UpperLimit(6.0),
                new Dimensions(7),
                new Data("8,9,10")));

        (var jsonString, _) = ObservationR4FhirJsonMapper.ToFhirJsonString(observation);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var sampledData = root.GetProperty("valueSampledData");

        Assert.Equal(2.0, sampledData.GetProperty("origin").GetProperty("value").GetDouble());
        Assert.Equal(3.0, sampledData.GetProperty("period").GetDouble());
        Assert.Equal(4.0, sampledData.GetProperty("factor").GetDouble());
        Assert.Equal(5.0, sampledData.GetProperty("lowerLimit").GetDouble());
        Assert.Equal(6.0, sampledData.GetProperty("upperLimit").GetDouble());
        Assert.Equal(7U, sampledData.GetProperty("dimensions").GetUInt32());
        Assert.Equal("8,9,10", sampledData.GetProperty("data").GetString());
    }

    [Fact]
    public void Observation_FromR4FhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }],
                    "text" : "Observation Code"
                },
                "valueSampledData" : {
                    "origin" : {
                        "value" : 2.0
                    },
                    "period" : 3.0,
                    "factor" : 4.0,
                    "lowerLimit" : 5.0,
                    "upperLimit" : 6.0,
                    "dimensions" : 7,
                    "data" : "8,9,10"
                }
            }
            """;

        (var observation, var outcomes) = ObservationR4FhirJsonMapper.ToObservationR4(jsonString);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(2.0, observation?.SampledData?.Origin.Value.Value);
        Assert.Equal(3.0, observation?.SampledData?.Period.Value);
        Assert.Equal(4.0, observation?.SampledData?.Factor.Value);
        Assert.Equal(5.0, observation?.SampledData?.LowerLimit.Value);
        Assert.Equal(6.0, observation?.SampledData?.UpperLimit.Value);
        Assert.Equal(7U, observation?.SampledData?.Dimensions.Value);
        Assert.Equal("8,9,10", observation?.SampledData?.Data.Value);
    }

    [Fact]
    public void Observation_ToR5FhirJsonGeneratesJsonString()
    {
        var observation = new ObservationR5(
            new Status("final"),
            new ObservationCode(new ObservationCodeCoding(
                new Code("8310-5"),
                new CodeSystem(new Uri("http://loinc.org")))),
            new SampledDataR5(
                new OriginQuantity(new OriginValue(2.0)),
                new Interval(3.0),
                new IntervalUnit("s"),
                new Factor(4.0),
                new LowerLimit(5.0),
                new UpperLimit(6.0),
                new Dimensions(7),
                new CodeMap(new Uri("http://example.com/")),
                new Offsets("1,2,3"),
                new Data("8,9,10")));

        (var jsonString, _) = ObservationR5FhirJsonMapper.ToFhirJsonString(observation, FhirVersion.R5);

        using var document = JsonDocument.Parse(jsonString!);
        JsonElement root = document.RootElement;

        var sampledData = root.GetProperty("valueSampledData");

        Assert.Equal(2.0, sampledData.GetProperty("origin").GetProperty("value").GetDouble());
        Assert.Equal(3.0, sampledData.GetProperty("interval").GetDouble());
        Assert.Equal("s", sampledData.GetProperty("intervalUnit").GetString());
        Assert.Equal(4.0, sampledData.GetProperty("factor").GetDouble());
        Assert.Equal(5.0, sampledData.GetProperty("lowerLimit").GetDouble());
        Assert.Equal(6.0, sampledData.GetProperty("upperLimit").GetDouble());
        Assert.Equal(7U, sampledData.GetProperty("dimensions").GetUInt32());
        Assert.Equal("http://example.com/", sampledData.GetProperty("codeMap").GetString());
        Assert.Equal("1,2,3", sampledData.GetProperty("offsets").GetString());
        Assert.Equal("8,9,10", sampledData.GetProperty("data").GetString());
    }

    [Fact]
    public void Observation_FromR5FhirJsonGeneratesRecords()
    {
        var jsonString = """
            {
                "resourceType" : "Observation",
                "status" : "final",
                "code" : {
                    "coding" : [{
                        "code" : "8310-5",
                        "system" : "http://loinc.org"
                    }],
                    "text" : "Observation Code"
                },
                "valueSampledData" : {
                    "origin" : {
                        "value" : 2.0
                    },
                "interval" : 3.0,
                "intervalUnit" : "s",
                "factor" : 4.0,
                "lowerLimit" : 5.0,
                "upperLimit" : 6.0,
                "dimensions" : 7,
                "codeMap" : "http://example.com/",
                "offsets" : "1,2,3",
                "data" : "8,9,10"
                }
            }
            """;

        (var observation, var outcomes) = ObservationR5FhirJsonMapper.ToObservationR5(jsonString, FhirVersion.R5);

        Assert.Empty(outcomes.Issues);
        Assert.Equal(2.0, observation?.SampledData?.Origin.Value.Value);
        Assert.Equal(3.0, observation?.SampledData?.Interval.Value);
        Assert.Equal("s", observation?.SampledData?.IntervalUnit.Value);
        Assert.Equal(4.0, observation?.SampledData?.Factor.Value);
        Assert.Equal(5.0, observation?.SampledData?.LowerLimit.Value);
        Assert.Equal(6.0, observation?.SampledData?.UpperLimit.Value);
        Assert.Equal(7U, observation?.SampledData?.Dimensions.Value);
        Assert.Equal("http://example.com/", observation?.SampledData?.CodeMap.Value.ToString());
        Assert.Equal("1,2,3", observation?.SampledData?.Offsets.Value);
        Assert.Equal("8,9,10", observation?.SampledData?.Data.Value);
    }
}
