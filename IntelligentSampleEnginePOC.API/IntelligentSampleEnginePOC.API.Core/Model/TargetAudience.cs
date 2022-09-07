using System.Text.Json.Serialization;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class TargetAudience
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("audienceNumber")]
        public int AudienceNumber { get; set; }
        
        [JsonPropertyName("estimatedIR")]
        public int EstimatedIR { get; set; }
        
        [JsonPropertyName("estimatedLOI")]
        public int EstimatedLOI { get; set; }
        
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        
        [JsonPropertyName("limitType")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? LimitType { get; set; }
        
        [JsonPropertyName("testingUrl")]
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? TestingUrl { get; set; }
        
        [JsonPropertyName("liveUrl")]
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? LiveUrl { get; set; }

        [JsonPropertyName("qualifications")]
        public List<Qualification> Qualifications { get; set; } = new();

        [JsonPropertyName("quotas")]
        public List<Quota> Quotas { get; set; } = new();
    }
}
