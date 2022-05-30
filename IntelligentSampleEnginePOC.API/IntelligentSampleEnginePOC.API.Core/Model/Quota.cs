using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Quota
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProjectQuotaType { get; set; }
        public int FieldTarget { get; set; }
        public int QuotaNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
