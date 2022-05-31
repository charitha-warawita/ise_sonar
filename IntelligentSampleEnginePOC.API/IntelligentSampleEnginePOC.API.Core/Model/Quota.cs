using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentSampleEnginePOC.API.Core.Model
{
    public class QuotaGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Quota> Quotas { get; set; }
    }

    public class Quota
    {
        public string QuotaName { get; set; }
        public int Limit { get; set; }
        public string Precode { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int Gender { get; set; }
    }
}
