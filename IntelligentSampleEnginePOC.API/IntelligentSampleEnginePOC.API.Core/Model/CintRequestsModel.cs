using IntelligentSampleEnginePOC.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class CintRequestModel 
    {
        public long ProjectId { get; set; }
        public long TargetAudienceId { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public bool ValidationResult { get; set; }
        public CintRequest Request { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> Errors { get; set; }
        
    }
}
