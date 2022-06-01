using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class TargetAudience
    {
        public TargetAudience()
        {
            ProjectTargetAudiences = new HashSet<ProjectTargetAudience>();
            TargetAudienceQualifications = new HashSet<TargetAudienceQualification>();
            TargetAudienceQuotaGroups = new HashSet<TargetAudienceQuotaGroup>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? Tanumber { get; set; }
        public int? Limit { get; set; }
        public string? LimitType { get; set; }

        public virtual ICollection<ProjectTargetAudience> ProjectTargetAudiences { get; set; }
        public virtual ICollection<TargetAudienceQualification> TargetAudienceQualifications { get; set; }
        public virtual ICollection<TargetAudienceQuotaGroup> TargetAudienceQuotaGroups { get; set; }
    }
}
