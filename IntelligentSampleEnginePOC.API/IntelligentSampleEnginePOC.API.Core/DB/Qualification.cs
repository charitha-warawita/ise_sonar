using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.Core.DB
{
    public partial class Qualification
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LogicalOperator { get; set; }
        public int? NumberOfRequiredConditions { get; set; }
        public bool? IsActive { get; set; }
        public string? PreCodes { get; set; }
        public int? QualificationOrder { get; set; }
        public string TargetAudienceId { get; set; } = null!;

        public virtual TargetAudience TargetAudience { get; set; } = null!;
    }
}
