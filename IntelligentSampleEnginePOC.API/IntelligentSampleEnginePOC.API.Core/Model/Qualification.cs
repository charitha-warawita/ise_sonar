using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Json=System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Qualification
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name"), Json.JsonIgnore(Condition = Json.JsonIgnoreCondition.WhenWritingNull)]
        public string? Name { get; set; }
        [JsonProperty(PropertyName = "order")]
        public int Order { get; set; }
        [JsonProperty(PropertyName = "logicalDecision"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string LogicalDecision { get; set; }
        [JsonProperty(PropertyName = "numberOfRequiredConditions"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int NumberOfRequiredConditions { get; set; }
        [JsonProperty(PropertyName = "isActive"), System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "question")]
        public Question Question { get; set; }

    }
}
