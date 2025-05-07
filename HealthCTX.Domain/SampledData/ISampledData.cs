using HealthCTX.Domain.Attributes;

namespace HealthCTX.Domain.SampledData;

[FhirElement]
[FhirProperty("origin", typeof(ISampledDataOrigin), Cardinality.Mandatory)]
[FhirProperty("period", typeof(ISampledDataPeriod), Cardinality.Mandatory, ToVersion: FhirVersion.R4)]
[FhirProperty("interval", typeof(ISampledDataInterval), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("intervalUnit", typeof(ISampledDataIntervalUnit), Cardinality.Mandatory, FromVersion: FhirVersion.R5)]
[FhirProperty("factor", typeof(ISampledDataFactor), Cardinality.Optional)]
[FhirProperty("lowerLimit", typeof(ISampledDataLowerLimit), Cardinality.Optional)]
[FhirProperty("upperLimit", typeof(ISampledDataUpperLimit), Cardinality.Optional)]
[FhirProperty("dimensions", typeof(ISampledDataDimensions), Cardinality.Mandatory)]
[FhirProperty("codeMap", typeof(ISampledDataCodeMap), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("offsets", typeof(ISampledDataOffsets), Cardinality.Optional, FromVersion: FhirVersion.R5)]
[FhirProperty("data", typeof(ISampledDataData), Cardinality.Optional)]
public interface ISampledData : IElement;
