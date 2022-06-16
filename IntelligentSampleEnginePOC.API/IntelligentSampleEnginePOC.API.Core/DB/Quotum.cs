using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.Core.DB
{
    public partial class Quotum
    {
        public string Id { get; set; } = null!;
        public string QuotaName { get; set; } = null!;
        public int? Limit { get; set; }
        public string? Condition { get; set; }
        public bool? IsActive { get; set; }
        public string TargetAudienceId { get; set; } = null!;

        public virtual TargetAudience TargetAudience { get; set; } = null!;
    }
}
