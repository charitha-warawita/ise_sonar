using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class QuotaGrouper
    {
        public List<string> Keywords { get; set; }
        public quota CintQuota { get; set; }
    }
}
