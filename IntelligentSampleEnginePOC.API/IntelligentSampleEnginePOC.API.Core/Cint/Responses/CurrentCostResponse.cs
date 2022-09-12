using System.Text.Json.Serialization;

namespace IntelligentSampleEnginePOC.API.Core.Cint.Responses;

public class CurrentCostResponse
{
    [JsonPropertyName("cost")]
    public Cost Cost { get; set; }
}

public class Cost
{
    [JsonPropertyName("amount")]
    public double Amount { get; set; }
    
    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}