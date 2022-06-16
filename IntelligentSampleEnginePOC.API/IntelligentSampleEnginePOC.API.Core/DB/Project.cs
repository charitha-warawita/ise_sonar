﻿using System;
using System.Collections.Generic;

namespace IntelligentSampleEnginePOC.API.Core.DB
{
    public partial class Project
    {
        public Project()
        {
            TargetAudiences = new HashSet<TargetAudience>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Reference { get; set; }
        public string? UserId { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? StartDate { get; set; }
        public int? FieldingPeriod { get; set; }
        public int? Status { get; set; }
        public string? LinkToSurvey { get; set; }
        public int? CintResponseId { get; set; }
        public string? CintSelfLink { get; set; }
        public string? CintCurrentCostLink { get; set; }
        public string? CintTestingLink { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<TargetAudience> TargetAudiences { get; set; }
    }
}
