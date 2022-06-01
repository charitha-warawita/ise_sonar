using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class QuotaGroup
    {
        public QuotaGroup()
        {
            QuotaGroupQuota = new HashSet<QuotaGroupQuotum>();
            TargetAudienceQuotaGroups = new HashSet<TargetAudienceQuotaGroup>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<QuotaGroupQuotum> QuotaGroupQuota { get; set; }
        public virtual ICollection<TargetAudienceQuotaGroup> TargetAudienceQuotaGroups { get; set; }
    }
}
