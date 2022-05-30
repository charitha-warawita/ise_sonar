using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class CintResponse
    {
        public Guid ProjectId { get; set; }
        public int CintResponseId { get; set; }
        public int CintResponseStatus { get; set; }
        public string CintSelfUrl { get; set; }
        public string CintCurrentCostUrl { get; set; }
        public string CintTestingUrl { get; set; }

    }
}
