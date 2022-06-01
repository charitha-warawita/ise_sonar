using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class TargetAudienceQualification
    {
        public string Id { get; set; } = null!;
        public string TargetAudienceId { get; set; } = null!;
        public string QualificationId { get; set; } = null!;

        public virtual Qualification Qualification { get; set; } = null!;
        public virtual TargetAudience TargetAudience { get; set; } = null!;
    }
}
