using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Json=System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Project
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }
        [JsonProperty(PropertyName = "user")]
        public User User { get; set; }
        [JsonProperty(PropertyName = "lastUpdate"), Json.JsonIgnore(Condition = Json.JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime? LastUpdate { get; set; }
        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty(PropertyName = "fieldingPeriod")]
        public int FieldingPeriod { get; set; }
        [JsonProperty(PropertyName = "status")]
        public Status Status { get; set; }
        [JsonProperty(PropertyName = "categories")]
        public List<int> Categories { get; set; }
        
        [JsonProperty(PropertyName = "targetAudiences")]
        public List<TargetAudience>? TargetAudiences { get; set; }
    }
}
