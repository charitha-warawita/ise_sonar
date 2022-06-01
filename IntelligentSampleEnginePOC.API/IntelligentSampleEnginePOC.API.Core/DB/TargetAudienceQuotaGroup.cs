using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class TargetAudienceQuotaGroup
    {
        public string Id { get; set; } = null!;
        public string TargetAudienceId { get; set; } = null!;
        public string QuotaGroupId { get; set; } = null!;

        public virtual QuotaGroup QuotaGroup { get; set; } = null!;
        public virtual TargetAudience TargetAudience { get; set; } = null!;
    }
}
