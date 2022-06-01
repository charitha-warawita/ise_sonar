using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class ProjectTargetAudience
    {
        public string Id { get; set; } = null!;
        public string ProjectId { get; set; } = null!;
        public string TargetAudienceId { get; set; } = null!;

        public virtual Project Project { get; set; } = null!;
        public virtual TargetAudience TargetAudience { get; set; } = null!;
    }
}
