using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.UI.Models
{
    public class TargetAudience
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int TANumber { get; set; }
        public int Limit { get; set; }
        public string LimitType { get; set; }
        public List<Qualification> Qualifications { get; set; }
        public List<Quota> Quotas { get; set; }
    }
}
