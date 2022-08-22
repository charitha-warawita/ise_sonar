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
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "limitType"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? LimitType { get; set; }
        [JsonProperty(PropertyName = "testingUrl"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string TestingUrl { get; set; }
        [JsonProperty(PropertyName = "liveUrl"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string LiveUrl { get; set; }
        [JsonProperty(PropertyName = "qualifications")]
        public List<Qualification> Qualifications { get; set; }
        [JsonProperty(PropertyName = "quotas")]
        public List<Quota> Quotas { get; set; }


    }
}
