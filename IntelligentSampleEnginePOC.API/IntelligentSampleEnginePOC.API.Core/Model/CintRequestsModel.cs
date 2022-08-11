using IntelligentSampleEnginePOC.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class CintRequestsModel 
    {
        public bool ValidationResult { get; set; }
        public List<string> Errors { get; set; }
        public List<CintRequest> Requests { get; set; }
    }
}
