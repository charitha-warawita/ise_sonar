using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class ProjectList
    {
        [JsonProperty(PropertyName = "totalItems")]
        public long TotalItems { get; set; }
        [JsonProperty(PropertyName = "projects")]
        public List<Project> Projects { get; set; }
    }
}
