using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.Core.DB
{
    public partial class TargetAudience
    {
        public TargetAudience()
        {
            Qualifications = new HashSet<Qualification>();
            Quota = new HashSet<Quotum>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Tanumber { get; set; }
        public int? Limit { get; set; }
        public string? LimitType { get; set; }
        public string ProjectId { get; set; } = null!;

        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<Qualification> Qualifications { get; set; }
        public virtual ICollection<Quotum> Quota { get; set; }
    }
}
