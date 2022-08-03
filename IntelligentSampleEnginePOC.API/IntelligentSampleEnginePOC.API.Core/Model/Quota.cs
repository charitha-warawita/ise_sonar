using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Quota
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "quotaName")]
        public string QuotaName { get; set; }
        [JsonProperty(PropertyName = "quotaType")]
        public string QuotaType { get; set; }
        [JsonProperty(PropertyName = "fieldTarget")]
        public int FieldTarget { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }
        [JsonProperty(PropertyName = "prescreens")]
        public int Prescreens { get; set; }
        [JsonProperty(PropertyName = "completes")]
        public int Completes { get; set; }
        [JsonProperty(PropertyName = "isActive")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "conditions")]
        public List<Question> Conditions { get; set; }
    }
}
