using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class Quota
    {
        public long Id { get; set; }
        public string QuotaName { get; set; }
        public string QuotaType { get; set; }
        public int FieldTarget { get; set; }
        public int Limit { get; set; }
        public int Prescreens { get; set; }
        public int Completes { get; set; }
        public bool IsActive { get; set; }
        public List<Question> Conditions { get; set; }
    }
}
