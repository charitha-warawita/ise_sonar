using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class TargetAudience
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int AudienceNumber { get; set; }
        public int EstimatedIR { get; set; }
        public int EstimatedLOI { get; set; }
        public int Limit { get; set; }
        public string LimitType { get; set; }
        public List<Qualification> Qualifications { get; set; }
        public List<Quota> Quotas { get; set; }
    }
}
