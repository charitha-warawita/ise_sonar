using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class TargetAudience
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "audienceNumber")]
        public int AudienceNumber { get; set; }
        [JsonProperty(PropertyName = "estimatedIR")]
        public int EstimatedIR { get; set; }
        [JsonProperty(PropertyName = "estimatedLOI")]
        public int EstimatedLOI { get; set; }
        [JsonProperty(PropertyName = "wantedCompletes")]
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "limitType"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? LimitType { get; set; }
        [JsonProperty(PropertyName = "qualifications")]
        public List<Qualification> Qualifications { get; set; }
        [JsonProperty(PropertyName = "quotas")]
        public List<Quota> Quotas { get; set; }
    }
}
