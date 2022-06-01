using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.DB
{
    public partial class Qualification
    {
        public Qualification()
        {
            TargetAudienceQualifications = new HashSet<TargetAudienceQualification>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? LogicalOperator { get; set; }
        public int? NumberOfRequiredConditions { get; set; }
        public bool? IsActive { get; set; }
        public string? PreCodes { get; set; }
        public int? QualificationOrder { get; set; }

        public virtual ICollection<TargetAudienceQualification> TargetAudienceQualifications { get; set; }
    }
}
