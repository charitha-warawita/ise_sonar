using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class Quotum
    {
        public Quotum()
        {
            QuotaGroupQuota = new HashSet<QuotaGroupQuotum>();
        }

        public string Id { get; set; } = null!;
        public string QuotaName { get; set; } = null!;
        public int? Limit { get; set; }
        public string? PreCode { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? Gender { get; set; }

        public virtual ICollection<QuotaGroupQuotum> QuotaGroupQuota { get; set; }
    }
}
